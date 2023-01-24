using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class MoveFish : Movement
{
    GameObject thisObj;
    Vector3 fishVector;
    Vector3 movementZX;
    Vector3 movementY;

    [SerializeField] CinemachineFreeLook cam;

    [SerializeField] LayerMask layerMask;

    [SerializeField] InventoryObject inventory;

    GameObject gameObj;
    // GameObject swapManager;

    float moveSpeed = 1f;

    void Start()
    {
        // swapManager =  GameObject.FindWithTag("SwapManager");
        // swapManager.GetComponent<SwapMoveControl>();
    }
    public override Vector3 Move(Vector3 direction)
    {
        // Vector3 move = Vector3.zero;
        // move += transform.forward;
        // transform.position += move * moveSpeed * Time.deltaTime;  
        // RayCastManager(gameObj);      
        // return move;

        if ((cam.LookAt = transform) && (cam.Follow = transform))
        {
            RayCastManager(gameObj);
            float horizontal = Input.GetAxisRaw("Horizontal");
            float vertical = Input.GetAxisRaw("Vertical");
            float forwardInput = Input.GetAxisRaw("Forward");

            // movement direction vector uses the inputs to determine the new X and Z positions
            // if i add in the Y component, it moves forward while also moving on Y
            movementZX = new Vector3(horizontal, 0f, forwardInput).normalized;

            movementY = new Vector3(0f, vertical, 0f).normalized;
            if (movementZX != Vector3.zero)
            {
                movementZX *= moveSpeed * Time.deltaTime;
            }
            if (movementY != Vector3.zero)
            {
                movementY *= moveSpeed * Time.deltaTime;
            }
        }
        return movementZX;
    }

    // Update is called once per frame
    void Update()
    {
        Move(fishVector);
    }

    public override GameObject RayCastManager(GameObject gameObj)
    {
        bool mouseClick = Input.GetKey(KeyCode.Mouse0);
        // ray from camera origin, pointing forwards
        Ray ray = new Ray (transform.position, cam.transform.forward);
        RaycastHit hit;
        // ray distance
        float distance = 3f;

        if (Physics.Raycast(ray, out hit, distance, layerMask))
        {
            // visual line for ray
            Debug.DrawRay(transform.position, cam.transform.forward * distance, Color.red);
            // store info of the hit gameobject if it has "item" script attached
            var item = hit.transform.GetComponent<Item>();
            gameObj = hit.transform.gameObject;
    
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
        return gameObj;
    }
}
