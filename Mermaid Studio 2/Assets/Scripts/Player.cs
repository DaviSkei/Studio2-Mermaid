using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    CharacterController controller;

    [SerializeField] Transform mainCam;

    [SerializeField] InventoryObject inventory;

    [SerializeField] float defaultSpeed = 1f;
    float startSpeed;
    
    float moveSpeedXZ = 3f;

    float moveSpeedY = 2f;

    float rotationTime = 0.25f;

    float rotationSpeed;

    [SerializeField] LayerMask layerMask;

    [SerializeField] GameObject CollectTrashUI;

    // Start is called before the first frame update
    void Start()
    {
        startSpeed = defaultSpeed;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        controller = GetComponent<CharacterController>();
        
        Debug.Log(inventory.name);
    }

    // Update is called once per frame
    void Update()
    {

        //Change later
        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }
        Movement();

        Sprint();

        RaycastManager();


    }

    void Movement()
    {
        //Get movement key input
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        float forwardInput = Input.GetAxisRaw("Forward");

        // movement direction vector uses the inputs to determine the new X and Z positions
        // if i add in the Y component, it moves forward while also moving on Y
        Vector3 movementXZ = new Vector3(horizontal, 0f, forwardInput).normalized;

        Vector3 movementY = new Vector3(0f, vertical, 0f);

        // movementXZ += mainCam.TransformDirection(movementY);


        // if movement does not equal zero, that means we are recieving input to move
        if (movementY != Vector3.zero)
        {
            // tell the controller move function to return the vector 3 value of the Y input
            controller.Move(movementY * moveSpeedY * Time.deltaTime);
        }
        
        // if movement on x or z axis is not nothing
        if (movementXZ != Vector3.zero)
        {

            // ability to rotate the players movement direction
            float targetAngle = Mathf.Atan2(movementXZ.x, movementXZ.z)
            * Mathf.Rad2Deg + mainCam.eulerAngles.y;

            // smooths the rotation movement on the player between its current rotation, to its intended rotation
            // which is based on the movement inputs
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref rotationSpeed, rotationTime);

            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 camDirection = Quaternion.Euler(mainCam.rotation.x, targetAngle, 0f) * Vector3.forward;
            controller.Move(camDirection.normalized * defaultSpeed * Time.deltaTime);

            defaultSpeed += moveSpeedXZ * Time.deltaTime;

            if (defaultSpeed > moveSpeedXZ)
            {
                defaultSpeed = moveSpeedXZ;
            }
        }
        else if ( movementXZ == Vector3.zero)
        {
            defaultSpeed = startSpeed;
        }
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
    // private void NormalizeSpeed(float speed)
    // {
    //     moveSpeedXZ = defaultSpeedXZ;
    // }
    void RaycastManager()
    {
        bool mouseClick = Input.GetKey(KeyCode.Mouse0);
        // ray from camera origin, pointing forwards
        Ray ray = new Ray (transform.position, mainCam.forward);
        RaycastHit hit;
        // ray distance
        float distance = 3f;

        CollectTrashUI.SetActive(false);

        // if ray hits something within the layermask
        if (Physics.Raycast(ray, out hit, distance, layerMask))
        {
            // visual line for ray
            Debug.DrawRay(transform.position, mainCam.forward * distance, Color.red);

            
            // store info of the hit gameobject if it has "item" script attached
            var item = hit.transform.gameObject.GetComponent<Item>();
            // store gameobject being hit by raycast
            GameObject gameObj = hit.transform.gameObject;

            // if hit gameobject has "Item" script attached show UI
            if (item)
            {
                CollectTrashUI.SetActive(true);


                // if player inputs left mouse click, add item data to inventory data
                // and destroy gameobject with the "item" script attached
                if (mouseClick)
                {
                    inventory.AddItem(item.item, 1);
                    Destroy(gameObj);
                }
            }
            
        }
    }

    private void OnApplicationQuit()
    {
        inventory.Container.Clear();
    }
}
