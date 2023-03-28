using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class MovePlayer : MonoBehaviour
{
    // player and camera components
    Rigidbody rbPlayer;
    public Rigidbody RbPlayer {get{return rbPlayer;}}

    [SerializeField] Transform mainCam;

    [SerializeField] CinemachineFreeLook camFreeLook;

    // [SerializeField] InventoryObject inventory;
    [SerializeField] InventoryObject playerInventory;

    // Movement variables
    float defaultSpeed = 1.5f;
    float startSpeed;
    float moveSpeedXZ = 3f;
    float moveSpeedY = 2f;
    float rotationTime = 0.2f;
    float rotationSpeed;
    
    // raycast variables
    [SerializeField] LayerMask layerMask;

    // variables to talk with npc
    bool inRangeOfNpc;
    [SerializeField] LayerMask npcLayer;
    float sphereDistance = 5f;

    // UI Elements
    [SerializeField] GameObject speak2npcUi;

    // animation variables
    bool isSwimming;
    bool isSwimmingUp;

    // Variables to swap movement with fish
    bool swapped;

    // raycasting variables
    Ray ray;
    RaycastHit hit;
    public RaycastHit Hit {get{return hit;}}
    float rayDistance = 5f;

    // Camera swap with NPC 
    DialogueManager dialogueManager;

    // Start is called before the first frame update
    void Start()
    {
        rbPlayer = GetComponent<Rigidbody>();

        startSpeed = defaultSpeed;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        camFreeLook.LookAt = this.transform;
        camFreeLook.Follow = this.transform;

        dialogueManager = FindObjectOfType<DialogueManager>();
    }

    // Update is called once per frame
    void Update()
    {
        Sprint();

        RayCastManager();

        NpcDialogue();

        CameraSwap();
    }
    void FixedUpdate()
    {
        bool return2Player = Input.GetKeyDown(KeyCode.F);

        //Change later
        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }
        if (!swapped)
        {
            Move();
        }
        if (return2Player)
        {
            camFreeLook.LookAt = this.transform;
            camFreeLook.Follow = this.transform;
            swapped = false;
        }
    }

    private void Move()
    {
        //Get movement key input
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        float forwardInput = Input.GetAxisRaw("Forward");

        // movement direction vector uses the inputs to determine the new X and Z positions
        Vector3 movementZX = new Vector3(horizontal, 0f, forwardInput).normalized;

        Vector3 movementY = new Vector3(0f, vertical, 0f).normalized;

        // if movement does not equal zero, that means we are recieving input to move
        if (movementY != Vector3.zero)
        {
            isSwimmingUp = true;
            // tell the controller move function to return the vector 3 value of the Y input
            // controller.Move(movementY * moveSpeedY * Time.deltaTime);
            rbPlayer.AddForce(movementY * moveSpeedY * Time.deltaTime, ForceMode.VelocityChange);
        }
        else if (movementY == Vector3.zero)
        {
            isSwimmingUp = false;
        }
        
        // if movement on x or z axis is not nothing
        if (movementZX != Vector3.zero)
        {
            isSwimming = true;
            
            float targetAngle = Mathf.Atan2(movementZX.x, movementZX.z)
            * Mathf.Rad2Deg + mainCam.eulerAngles.y;

            // smooths the rotation movement on the player between its current rotation, to its intended rotation
            // which is based on the movement inputs
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref rotationSpeed, rotationTime);

            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 camDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            // controller.Move(camDirection.normalized * defaultSpeed * Time.deltaTime);
            rbPlayer.AddForce(camDirection.normalized * defaultSpeed * Time.deltaTime, ForceMode.VelocityChange);

            // movespeed increases over time
            defaultSpeed += moveSpeedXZ * Time.deltaTime;

            // once the speed reaches the limit we want, then set it to become that limit
            if (defaultSpeed > moveSpeedXZ)
            {
                defaultSpeed = moveSpeedXZ;
            }
        }
        else if ( movementZX == Vector3.zero)
        {
            isSwimming = false;
            defaultSpeed = startSpeed;
        }
    }
    
    private void Sprint()
    {
        bool sprintInput = Input.GetKey(KeyCode.LeftShift);
        if (sprintInput)
        {
            // fast swim animation bool true
            moveSpeedXZ = 7f;
            moveSpeedY = 4f;
        }
        if (!sprintInput)
        {
            // fast swim animation bool false
            moveSpeedXZ = 3f;
            moveSpeedY = 2f;
        }
    }
    // this function is called from EquiptmentLogic script
    public void GravityLogic()
    {
        rbPlayer.AddForce(new Vector3(0, -0.7f, 0), ForceMode.Acceleration);
    }
    
    public void RayCastManager()
    {
        bool mouseClickDown = Input.GetKeyDown(KeyCode.Mouse0);
        bool mouseClickUp = Input.GetKeyUp(KeyCode.Mouse0);
        bool mouseClick = Input.GetKey(KeyCode.Mouse0);

        ray = new Ray (transform.position, mainCam.forward);

        inRangeOfNpc = Physics.CheckSphere(transform.position, sphereDistance, npcLayer);

        if (Physics.Raycast(ray, out hit, rayDistance, layerMask))
        {
            Debug.DrawRay(transform.position, mainCam.forward * rayDistance, Color.red);
            
            var item = hit.transform.GetComponent<Item>();

            MoveFish fish = hit.transform.gameObject.GetComponent<MoveFish>();

            GameObject itemObj = hit.transform.gameObject;

            if (fish && Input.GetKey(KeyCode.Q))
            {
                camFreeLook.LookAt = fish.transform;
                camFreeLook.Follow = fish.transform;
                swapped = true;
                fish.SetBool(swapped);
            }
            if (item)
            {
                if (mouseClick)
                {
                    playerInventory.AddItem(item.ItemObject(), item.ItemObject().itemAmount, item.ItemObject().itemWeight);
                    Destroy(itemObj);
                }
            }
        }
    }
    private void NpcDialogue()
    {
        if (inRangeOfNpc)
        {
            ShowCursor();
            speak2npcUi.SetActive(true);
        }
        else
        {
            HideCursor();
            speak2npcUi.SetActive(false);
        }
    }
    private void CameraSwap()
    {
        if (dialogueManager.IsTalking)
        {
            camFreeLook.enabled = false;
        }
        else
        {
            camFreeLook.enabled = true;
        }
    }
    public bool IsSwimming()
    {
        return isSwimming;
    }
    public bool IsSwimmingUp()
    {
        return isSwimmingUp;
    }
    private void ShowCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    private void HideCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
