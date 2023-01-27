using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class MoveFish : MonoBehaviour
{
    GameObject thisObj;
    Vector3 fishVector;
    Vector3 movementZX;
    Vector3 movementY;

    // CinemachineFreeLook cam;

    Camera mainCam;

    [SerializeField] LayerMask layerMask;

    [SerializeField] InventoryObject inventory;

    GameObject itemObj;

    // vars for player control
    float moveSpeed = 5f;
    float rotationSpeed;
    float rotationTime = 0.2f;

    public bool ctrlByPlayer = false;

    void Start()
    {
        // cam = Transform.FindObjectOfType<CinemachineFreeLook>();

        mainCam = Camera.main;

        // delete later
        FishMove();
    }

    // Update is called once per frame
    void Update()
    {
        bool return2Player = Input.GetKeyDown(KeyCode.F);

        if (ctrlByPlayer == true)
        {
            Debug.Log(ctrlByPlayer);
            ControlMove();
        }
        if (return2Player)
        {
            ctrlByPlayer = false;
            Debug.Log(ctrlByPlayer);
            // boid control later
            FishMove();
        }
    }
    private void ControlMove()
    {
        // Vector3 move = Vector3.zero;
        // move += transform.forward;
        // transform.position += move * moveSpeed * Time.deltaTime;  
        // RayCastManager(gameObj);      
        // return move;

            RayCastManager();
            float horizontal = Input.GetAxisRaw("Horizontal");
            float vertical = Input.GetAxisRaw("Vertical");
            float forwardInput = Input.GetAxisRaw("Forward");

            // movement direction vector uses the inputs to determine the new X and Z positions
            // if i add in the Y component, it moves forward while also moving on Y
            movementZX = new Vector3(horizontal, 0f, forwardInput).normalized;

            movementY = new Vector3(0f, vertical, 0f).normalized;

            if (movementZX != Vector3.zero)
            {
                // ability to rotate the players movement direction
                float targetAngle = Mathf.Atan2(movementZX.x, movementZX.z)
                * Mathf.Rad2Deg + mainCam.transform.eulerAngles.y;

                // smooths the rotation movement on the player between its current rotation, to its intended rotation
                // which is based on the movement inputs
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref rotationSpeed, rotationTime);

                transform.rotation = Quaternion.Euler(0f, angle, 0f);

                Vector3 camDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
                
                transform.position += camDirection.normalized * moveSpeed * Time.deltaTime;
            }
            if (movementY != Vector3.zero)
            {
                transform.position += movementY * moveSpeed * Time.deltaTime;
            }
    }
    void FishMove()
    {
        transform.position += transform.forward * Time.deltaTime;
    }

    private void RayCastManager()
    {
        bool mouseClick = Input.GetKey(KeyCode.Mouse0);
        // ray from camera origin, pointing forwards
        Ray ray = new Ray (transform.position, transform.forward);
        RaycastHit hit;
        // ray distance
        float distance = 3f;

        if (Physics.Raycast(ray, out hit, distance, layerMask))
        {
            // visual line for ray
            Debug.DrawRay(transform.position, hit.point * distance, Color.red);
            // store info of the hit gameobject if it has "item" script attached
            var item = hit.transform.GetComponent<Item>();
            itemObj = hit.transform.gameObject;
    
            // if hit gameobject has "Item" script attached show UI
            if (item)
            {
    
                // if player inputs left mouse click, add item data to inventory data
                // and destroy gameobject with the "item" script attached
                if (mouseClick)
                {
                    inventory.AddItem(item.item, 1);
                    Destroy(itemObj);
                }
            }
        }
    }
}
