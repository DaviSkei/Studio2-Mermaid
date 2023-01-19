using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    CharacterController controller;

    [SerializeField] Transform mainCam;

    [SerializeField] InventoryObject inventory;

    float moveSpeedXZ = 3f;

    float moveSpeedY = 2f;

    float rotationTime = 0.1f;

    float rotationSpeed;

    [SerializeField] LayerMask layerMask;

    [SerializeField] GameObject CollectTrashUI;

    // Start is called before the first frame update
    void Start()
    {
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

        /* bool seeTutorial = Input.GetKeyDown(KeyCode.Tab);
        bool turnTutorialOff = Input.GetKeyUp(KeyCode.Tab);
        bool tutorialOn = false;
        float timer = 0f;
        if (!tutorialOn && seeTutorial)
        {
            Tab4Keybinds.SetActive(false);
            ControlMenu.SetActive(true);
            tutorialOn = true;
            timer += Time.deltaTime;
        }
        if (tutorialOn && turnTutorialOff && timer > 1f)
        {
            Tab4Keybinds.SetActive(true);
            ControlMenu.SetActive(false);
            tutorialOn = false;
            timer = timer - Time.deltaTime;
            timer = 0f;
        }
        */


    }

    void Movement()
    {
        //Get movement key input
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        float forwardInput = Input.GetAxisRaw("Forward");

        // movement direction vector uses the inputs to determine the new X, Y and Z positions
        Vector3 movementXZ = new Vector3(horizontal, vertical, forwardInput).normalized;

        Vector3 movementY = new Vector3(0f, vertical, 0f);

        // if movement on x or z axis is not nothing
        if (movementY != Vector3.zero)
        {
            // tell the controller move function to return the vector 3 value of the Y input
            controller.Move(movementY * moveSpeedY * Time.deltaTime);
        }
        
        // if movement on x or z axis is not nothing
        if (movementXZ != Vector3.zero)
        {

            // ability to shift movement direction based on camera direction
            float targetAngle = Mathf.Atan2(movementXZ.x, movementXZ.z)
            * Mathf.Rad2Deg + mainCam.eulerAngles.y;

            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref rotationSpeed, rotationTime);

            transform.rotation = Quaternion.Euler(0f, angle, 0f);
            

            Vector3 camDirection = Quaternion.Euler(0f, targetAngle, targetAngle) * Vector3.forward;
            controller.Move(camDirection.normalized * moveSpeedXZ * Time.deltaTime);
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
    //     moveSpeedXZ = startMoveSpeedXZ;
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
