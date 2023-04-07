using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turtle : MonoBehaviour
{
    Animator turtleAnimator;
    [SerializeField] private float moveSpeed = 0.5f;
    [SerializeField] private float rotateSpeed = 2f;

    [SerializeField] private bool isTrapped = false;
    
    GameObject netObj;

    private string inNet = "inNet";
    // Start is called before the first frame update
    void Start()
    {
        turtleAnimator = GetComponent<Animator>();
        netObj = GetComponentInChildren<FishNet>().gameObject;
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
    }
    private void TrappedLogic()
    {
        turtleAnimator.SetBool(inNet, true);
        netObj.SetActive(true);
    }
    void Move()
    { 
        transform.position += transform.forward * moveSpeed * Time.deltaTime;
        transform.Rotate(Vector3.up * (rotateSpeed * Time.deltaTime));
        turtleAnimator.SetBool(inNet, false);
        netObj.SetActive(false);
    }

}
