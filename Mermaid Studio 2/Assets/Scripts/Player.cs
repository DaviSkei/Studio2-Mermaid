using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] CharacterController controller;

    [SerializeField] Transform mainCam;

    [SerializeField] InventoryObject inventory;

    float moveSpeed = 3f;

    float moveSpeedUpDown = 2f;

    float rotationTime = 0.1f;

    float rotationSpeed;

    [SerializeField] LayerMask layerMask;

    [SerializeField] GameObject Tab4Keybinds;
    [SerializeField] GameObject ControlMenu;
    [SerializeField] GameObject CollectTrashUI;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
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
        
        bool seeTutorial = Input.GetKeyDown(KeyCode.Tab);
        if (seeTutorial)
        {
            Tab4Keybinds.SetActive(false);
            ControlMenu.SetActive(true);
        }
        else if (!Tab4Keybinds && ControlMenu && seeTutorial)
        {
            Tab4Keybinds.SetActive(true);
            ControlMenu.SetActive(false);
        }
        */


    }

    void Movement()
    {
        //Get movement key input
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        float forwardInput = Input.GetAxisRaw("Forward");

        Vector3 movementDirection = new Vector3(horizontal, vertical, forwardInput).normalized;

        Vector3 MovementUpDown = new Vector3(0f, vertical, 0f);

        // movement for vertical
        if (MovementUpDown != Vector3.zero)
        {
            controller.Move(MovementUpDown * moveSpeedUpDown * Time.deltaTime);
        }
        
        // movement for horizontal + forward
        if (movementDirection != Vector3.zero)
        {

            // ability to shift movement direction based on camera direction
            // was x then z, changing now
            float targetAngle = Mathf.Atan2(movementDirection.x, movementDirection.z)
            * Mathf.Rad2Deg + mainCam.eulerAngles.y;

            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref rotationSpeed, rotationTime);

            transform.rotation = Quaternion.Euler(0f, angle, 0f);
            

            Vector3 camDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(camDirection.normalized * moveSpeed * Time.deltaTime);
        }
    }

    void Sprint()
    {
        // sprint function
            bool sprintInput = Input.GetKey(KeyCode.LeftShift);

            if (sprintInput)
            {
                moveSpeed = 7f;
                moveSpeedUpDown = 4f;
            } 
            else if (!sprintInput)
            {
                moveSpeed = 3f;
                moveSpeedUpDown = 2f;
            }
    }
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
