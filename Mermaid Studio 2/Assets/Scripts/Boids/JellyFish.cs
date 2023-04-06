using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JellyFish : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 3f,distance = 3f, fastTime;

    bool inRange;
    [SerializeField] private LayerMask layerMask;

    [SerializeField] Collider[] objects;

    bool didHit;
    RaycastHit hit;
    Vector3 avoidObst;
    Vector3 velocity;
    int avoidCount = 0;
    // Start is called before the first frame update
    void Start()
    {
        didHit = false;
        velocity = new Vector3(0,0,0);
        fastTime = Time.deltaTime*2;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.up * moveSpeed * Time.deltaTime;
        // inRange = Physics.CheckSphere(transform.position, distance, layerMask);
        // objects = Physics.OverlapSphere(transform.position, distance, layerMask);
        // transform.position += transform.up * moveSpeed* Time.deltaTime;
        // if (inRange)
        // {
        //     Vector3 steer = new Vector3(0,0,0);
        //     for (int i = 0; i < objects.Length; i++)
        //     {
        //         steer += transform.position - objects[i].transform.position/objects.Length;
        //         steer.Normalize();
        //         // transform.Rotate(steer);
        //     }
        // }

        if (Physics.Raycast(transform.position, transform.forward, out hit, distance, layerMask, QueryTriggerInteraction.Collide))
            {
                Debug.DrawRay(transform.position, transform.forward * hit.distance, Color.red);
                accumAvoid(hit.point);
                Move();
    
                didHit = true;
            }
            if (Physics.Raycast(transform.position, -transform.up, out hit, distance, layerMask, QueryTriggerInteraction.Collide))
            {
                Debug.DrawRay(transform.position, -transform.up * hit.distance, Color.red);
                accumAvoid(hit.point);
                Move();
    
                didHit = true;
            }
            if (Physics.Raycast(transform.position, transform.up, out hit, distance, layerMask, QueryTriggerInteraction.Collide))
            {
                Debug.DrawRay(transform.position, transform.up * hit.distance, Color.red);
                accumAvoid(hit.point);
                Move();
    
                didHit = true;
            }
            if (Physics.Raycast(transform.position, transform.right, out hit, distance, layerMask, QueryTriggerInteraction.Collide))
            {
                Debug.DrawRay(transform.position, transform.right * hit.distance, Color.red);
                accumAvoid(hit.point);
                Move();
    
                didHit = true;
            }
            if (Physics.Raycast(transform.position, -transform.right, out hit, distance, layerMask, QueryTriggerInteraction.Collide))
            {
                Debug.DrawRay(transform.position, -transform.right * hit.distance, Color.red);
                accumAvoid(hit.point);
                Move();
    
                didHit = true;
            }
    }
    private void accumAvoid(Vector3 avoid)
    {
        avoidObst += transform.position - avoid;
        avoidCount++;
    }
    private Vector3 avoid()
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
        newVelocity += avoid();
        Vector3 slerpVelo = Vector3.Slerp(velocity, newVelocity, fastTime);
    
        velocity = slerpVelo.normalized;
    
        transform.position += velocity * Time.deltaTime * moveSpeed;
        transform.LookAt(transform.position + velocity);
        transform.Rotate(velocity);
    }
}
