using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class MovePlayer : MonoBehaviour
{
    CharacterController controller;

    [SerializeField] Transform mainCam;

    [SerializeField] InventoryObject inventory;

    GameObject gameObj;
    GameObject hitObj;

    float defaultSpeed = 1.5f;
    float startSpeed;
    
    float moveSpeedXZ = 3f;

    float moveSpeedY = 2f;

    float rotationTime = 0.2f;

    float rotationSpeed;
    Vector3 thisVector;

    [SerializeField] LayerMask layerMask;

    [SerializeField] GameObject CollectTrashUI;

    // Start is called before the first frame update
    void Start()
    {
        startSpeed = defaultSpeed;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {

        //Change later
        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }
        Move(thisVector);

        Sprint();

        RayCastManager(hitObj);


    }

    private Vector3 Move(Vector3 direction)
    {
        //Get movement key input
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        float forwardInput = Input.GetAxisRaw("Forward");

        // movement direction vector uses the inputs to determine the new X and Z positions
        // if i add in the Y component, it moves forward while also moving on Y
        Vector3 movementZX = new Vector3(horizontal, 0f, forwardInput).normalized;

        Vector3 movementY = new Vector3(0f, vertical, 0f).normalized;

        // if movement does not equal zero, that means we are recieving input to move
        if (movementY != Vector3.zero)
        {
            // tell the controller move function to return the vector 3 value of the Y input
            controller.Move(movementY * moveSpeedY * Time.deltaTime);
        }
        
        // if movement on x or z axis is not nothing
        if (movementZX != Vector3.zero)
        {

            // ability to rotate the players movement direction
            float targetAngle = Mathf.Atan2(movementZX.x, movementZX.z)
            * Mathf.Rad2Deg + mainCam.eulerAngles.y;

            // smooths the rotation movement on the player between its current rotation, to its intended rotation
            // which is based on the movement inputs
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref rotationSpeed, rotationTime);

            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 camDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(camDirection.normalized * defaultSpeed * Time.deltaTime);



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
            defaultSpeed = startSpeed;
        }
        return movementZX;
    }
    
    private void Sprint()
    {
        bool sprintInput = Input.GetKey(KeyCode.LeftShift);
        if (sprintInput)
        {
            moveSpeedXZ = 7f;
            moveSpeedY = 4f;
        }
        if (!sprintInput)
        {
            moveSpeedXZ = 3f;
            moveSpeedY = 2f;
        }
    }
    
    public GameObject RayCastManager(GameObject gameObj)
    {
        bool mouseClick = Input.GetKey(KeyCode.Mouse0);
        // ray from camera origin, pointing forwards
        Ray ray = new Ray (transform.position, mainCam.forward);
        RaycastHit hit;
        // ray distance
        float distance = 3f;

        // CollectTrashUI.SetActive(false);
        if (Physics.Raycast(ray, out hit, distance, layerMask))
        {
            // visual line for ray
            Debug.DrawRay(transform.position, mainCam.forward * distance, Color.red);
            // store info of the hit gameobject if it has "item" script attached
            var item = hit.transform.GetComponent<Item>();

            hitObj = new GameObject();
            gameObj = hit.transform.gameObject;

            hitObj = gameObj;

            Debug.Log("player hit " + gameObj);
    
            // if hit gameobject has "Item" script attached show UI
            if (item)
            {
    
                // if player inputs left mouse click, add item data to inventory data
                // and destroy gameobject with the "item" script attached
                if (mouseClick)
                {
                    inventory.AddItem(item.item, 1);
                    Destroy(gameObj);
                }
            }
        }
        return hitObj;
    }

    private void OnApplicationQuit()
    {
        inventory.Container.Clear();
    }
}
