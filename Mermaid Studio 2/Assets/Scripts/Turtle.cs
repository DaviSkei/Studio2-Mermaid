using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turtle : MonoBehaviour
{
    Animator turtleAnimator;
    [SerializeField] private float moveSpeed = 0.5f;
    [SerializeField] private float rotateSpeed = 2f;

    [SerializeField] private bool isTrapped = false;

    Vector3 avoidObst;

    Vector3 velocity;
    int avoidCount = 0;

    private string inNet = "inNet";
    // Start is called before the first frame update
    void Start()
    {
        turtleAnimator = GetComponent<Animator>();
        velocity = new Vector3(0,0,0);
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
            MoveLogic();
            Move();
        }
    }
    private void TrappedLogic()
    {
        turtleAnimator.SetBool(inNet, true);
    }
    // private void Movement()
    // {
    //     isTrapped = false;
    //     transform.position += transform.forward * moveSpeed * Time.deltaTime;
    //     transform.Rotate(Vector3.up * (rotateSpeed * Time.deltaTime));
    // }
    private void MoveLogic()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;
        float rayDist = 10f;

        if (Physics.Raycast(ray, out hit, rayDist))
        {
            AccumAvoid(hit.point);
            Move();
        }
    }
    private void AccumAvoid(Vector3 avoid)
    {
        avoidObst += transform.position - avoid;
        avoidCount++;
    }
    private Vector3 Avoid()
    {

        if (avoidCount > 0)
        {
            return (avoidObst / avoidCount).normalized ;
        }

        return Vector3.zero;
    }
    private void Move()
    {
        Vector3 newVelocity = new Vector3(0,0,0);
        newVelocity += Avoid();
        Vector3 slerpVelo = Vector3.Slerp(velocity, newVelocity, Time.deltaTime);
    
        velocity = slerpVelo.normalized;
    
        transform.position += velocity * Time.deltaTime * moveSpeed;
        transform.LookAt(transform.position + velocity);
        transform.Rotate(velocity);
    }
}
