using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turtle : MonoBehaviour
{
    Animator turtleAnimator;
    [SerializeField] private float moveSpeed = 0.5f;
    [SerializeField] private float rotateSpeed = 2f;

    float multRotSpeed, slowRotSpeed;

    [SerializeField] private bool isTrapped = false;
    
    FishNet net;

    private string inNet = "inNet";

    RaycastHit hit;
    float rayDist = 7f;
    LayerMask layerMask = 1 << 10;

    // Start is called before the first frame update
    void Start()
    {
        multRotSpeed = rotateSpeed * 6;
        slowRotSpeed = rotateSpeed * 0.2f;

        turtleAnimator = GetComponent<Animator>();
        net = GetComponentInChildren<FishNet>();
        if(isTrapped)
        {
            TrappedLogic();
        }
        else
        {
            net.gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isTrapped)
        {
            TrappedLogic();
        }
        else
        {
            Move();
        }
        if (net.isCut)
        {
            isTrapped = false;
        }
    }
    private void TrappedLogic()
    {
        turtleAnimator.SetBool(inNet, true);
        net.gameObject.SetActive(true);
    }
    void Move()
    { 
        turtleAnimator.SetBool(inNet, isTrapped);
        if (RayManager())
        {
            MoveForward();
            transform.Rotate((Vector3.up + (-Vector3.right)) * (multRotSpeed * Time.deltaTime));
        }
        else
        {
            MoveForward();
            
            transform.Rotate(Vector3.up * (rotateSpeed * Time.deltaTime));
            transform.Rotate(Vector3.right * (slowRotSpeed * Time.deltaTime));
        }
    }
    void MoveForward()
    {
        transform.position += transform.forward * moveSpeed * Time.deltaTime;
    }
    bool RayManager()
    {
        // down ray
        if (Physics.Raycast(transform.position, -transform.up, out hit, rayDist, layerMask.value))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

}
