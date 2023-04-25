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

    Rigidbody rbFish;

    Camera mainCam;

    [SerializeField] LayerMask layerMask;

    [SerializeField] InventoryObject playerInventory;

    GameObject itemObj;

    // vars for player control
    float moveSpeed = 10f;
    float rotationSpeed;
    float rotationTime = 0.2f;

    bool ctrlByPlayer = false;
    SpiralSwarm fishControl;

    float timer = 0;

    void Start()
    {
        mainCam = Camera.main;

        fishControl = GetComponent<SpiralSwarm>();
    }

    // Update is called once per frame
    void Update()
    {
        bool return2Player = Input.GetKeyDown(KeyCode.F);

        if (ctrlByPlayer == true)
        {
            ControlMove();
            fishControl.enabled = false;
        }
        if (return2Player)
        {
            Destroy(rbFish);
            ctrlByPlayer = false;
            fishControl.enabled = true;
        }
    }
    private void ControlMove()
    {
        RayCastManager();
        gameObject.AddComponent<Rigidbody>();
        rbFish = GetComponent<Rigidbody>();

        rbFish.drag = 1f;
        rbFish.useGravity = false;
        rbFish.freezeRotation = true;
        rbFish.interpolation = RigidbodyInterpolation.Interpolate;
        rbFish.collisionDetectionMode = CollisionDetectionMode.Continuous;

        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        float forwardInput = Input.GetAxisRaw("Forward");

        // movement direction vector uses the inputs to determine the new X and Z positions
        // if i add in the Y component, it moves forward while also moving on Y
        movementZX = new Vector3(0f, 0f, forwardInput).normalized;

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

            rbFish.AddForce(camDirection.normalized * moveSpeed * Time.deltaTime, ForceMode.VelocityChange);
                
            // transform.position += camDirection.normalized * moveSpeed * Time.deltaTime;
        }
        if (movementY != Vector3.zero)
        {
            rbFish.AddForce(movementY.normalized * moveSpeed * Time.deltaTime, ForceMode.VelocityChange);
        }
    }

    private void RayCastManager()
    {
        bool mouseClick = Input.GetKey(KeyCode.Mouse0);
        // ray from camera origin, pointing forwards
        Ray ray = new Ray (transform.position, transform.forward);
        RaycastHit hit;
        // ray distance
        float distance = 5f;

        if (Physics.Raycast(ray, out hit, distance, layerMask))
        {
            // store info of the hit gameobject if it has "item" script attached
            Item item = hit.transform.GetComponent<Item>();
            itemObj = hit.transform.gameObject;

            DissolveController dissolve = hit.transform.GetComponent<DissolveController>();
    
            if (item)
            {
                if (mouseClick)
                {
                    dissolve.hitByPlayer = true;
                    playerInventory.AddItem(item.ItemObject(), item.ItemObject().itemAmount, item.ItemObject().itemWeight);
                    if (timer >= 0.1f)
                    {
                        Destroy(itemObj);
                        timer -= Time.deltaTime;
                        timer *= 0;
                    }
                }
            }
        }
    }
    public bool SetBool(bool x)
    {
        ctrlByPlayer = x;
        return ctrlByPlayer;
    }
}
