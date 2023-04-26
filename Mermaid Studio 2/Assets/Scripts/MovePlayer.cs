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
    [SerializeField] float defaultSpeed;
    float startSpeed;
    float moveSpeedXZ;
    float moveSpeedY;
    float rotationTime = 0.2f;
    float rotationSpeed;
    
    // raycast variables
    [SerializeField] LayerMask layerMask;

    // UI Elements
    [SerializeField] Animator inventoryAnimator;

    // animation variables
    bool isSwimming;
    public bool isGrabbing {get; private set;}

    // Variables to swap movement with fish
    bool swapped;

    // raycasting variables
    Ray ray;
    RaycastHit hit;

    float rayDistance = 6f;

    public bool canPickUp {get; private set;}

    // Camera swap with NPC 
    DialogueManager dialogueManager;

    // swimming particles
    [SerializeField] GameObject swimParticles;

    Collider[] colliders;
    bool inRange;
    float rangeDist = 250f;
    float colliderDist = 300f;
    [Header("add things that should spawn when close to player")]
    [SerializeField] LayerMask spawnabaleLayermask;

    void Start()
    {
        rbPlayer = GetComponent<Rigidbody>();

        startSpeed = defaultSpeed;
        moveSpeedXZ = defaultSpeed * 2;
        moveSpeedY = defaultSpeed * 1.5f;

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

        CameraSwap();

        // SpawnObjects();
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
    // void SpawnObjects()
    // {
    //     colliders = Physics.OverlapSphere(transform.position, colliderDist, spawnabaleLayermask, QueryTriggerInteraction.Collide);
    //     inRange = Physics.CheckSphere(transform.position, rangeDist, spawnabaleLayermask, QueryTriggerInteraction.Collide);

    //     for (int i = 0; i < colliders.Length; i++)
    //     {
    //         if (inRange)
    //         {
    //             colliders[i].transform.gameObject.SetActive(true);
    //         }
    //         else
    //         {
    //             colliders[i].transform.gameObject.SetActive(false);
    //         }
    //     }
    // }

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
            rbPlayer.AddForce(movementY * moveSpeedY * Time.deltaTime, ForceMode.VelocityChange);
        }
        // if movement on x or z axis is not nothing
        if (movementZX != Vector3.zero)
        {            
            float targetAngleY = Mathf.Atan2(movementZX.x, movementZX.z)
            * Mathf.Rad2Deg + mainCam.eulerAngles.y;
            

            // smooths the rotation movement on the player between its current rotation, to its intended rotation
            // which is based on the movement inputs
            float angleY = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngleY, ref rotationSpeed, rotationTime);

            transform.rotation = Quaternion.Euler(0,angleY, 0);

            Vector3 camDirection = Quaternion.Euler(0, targetAngleY, 0f) * Vector3.forward;
            
            rbPlayer.AddForce(camDirection.normalized * defaultSpeed * Time.deltaTime, ForceMode.VelocityChange);
        }
        if (movementZX != Vector3.zero || movementY != Vector3.zero)
        {
            isSwimming = true;
            swimParticles.SetActive(true);
        }
        else
        {
            isSwimming = false;
            defaultSpeed = startSpeed;
            swimParticles.SetActive(false);
        }
    }
    
    private void Sprint()
    {
        bool sprintInput = Input.GetKey(KeyCode.LeftShift);
        if (sprintInput)
        {
            defaultSpeed += moveSpeedXZ * Time.deltaTime;

            if (defaultSpeed > moveSpeedXZ)
            {
                defaultSpeed = moveSpeedXZ;
            }
        }
        else
        {
            defaultSpeed = startSpeed;
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
        canPickUp = false;
        isGrabbing = false;

        if (Physics.Raycast(ray, out hit, rayDistance, layerMask))
        {   
            Item item = hit.transform.GetComponent<Item>();

            GameObject itemObj = hit.transform.gameObject;
            
            MoveFish fish = hit.transform.gameObject.GetComponent<MoveFish>();

            if (fish && Input.GetKey(KeyCode.Q))
            {
                camFreeLook.LookAt = fish.transform;
                camFreeLook.Follow = fish.transform;
                swapped = true;
                fish.SetBool(swapped);
            }
            if (item)
            {
                canPickUp = true;
                if (mouseClickDown)
                {
                }
                if (mouseClick)
                {
                    playerInventory.AddItem(item.ItemObject(), item.ItemObject().itemAmount, item.ItemObject().itemWeight);
                    isGrabbing = true;
                    Destroy(itemObj); 
                }
            }
            else
            {
                isGrabbing = false;
            }
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
    public void ShowCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    public void HideCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
