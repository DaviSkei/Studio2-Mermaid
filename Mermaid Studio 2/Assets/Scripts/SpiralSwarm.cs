using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiralSwarm : MonoBehaviour
{
    float moveSpeed, rotateSpeed;

    [Range(1, 30)]
    [SerializeField] float minMoveSpeed = 8, maxMoveSpeed = 12;
    [Range(1, 30)]
    [SerializeField] float minRotSpeed = 8, maxRotSpeed = 12;

    RaycastHit hit;
    private float rayDist = 5f;
    LayerMask layerMask = 1<< 10;

    private int avoidCount = 0;
    Vector3 avoidObst;
    Vector3 newDir;
    Vector3 velocity;

    float slowtime;
    float timer = 0;

    void Start()
    {
        moveSpeed = Random.Range(minMoveSpeed, maxMoveSpeed);
        rotateSpeed = Random.Range(minRotSpeed, maxRotSpeed);
        newDir = new Vector3(0,0,0);
        velocity = new Vector3(0,0,0);

        slowtime = Time.deltaTime/10;
    }
    void Update()
    {
        Move();
    }
    void Move()
    { 
        if (RayManager())
        {
            timer += slowtime;
            newDir += Avoid();
            velocity = Vector3.Lerp(velocity, newDir, timer);
            // transform.position += velocity;
            transform.position += velocity.normalized + transform.forward * moveSpeed * Time.deltaTime;
            transform.Rotate(Vector3.up * (rotateSpeed * Time.deltaTime));
        }
        else
        {
            timer -= slowtime;
            timer = 0f;
            transform.position += transform.forward * moveSpeed * Time.deltaTime;
            transform.Rotate(Vector3.up * (rotateSpeed * Time.deltaTime));
        }
    }
    bool RayManager()
    {
        // down ray
        if (Physics.Raycast(transform.position, -transform.up, out hit, rayDist, layerMask.value))
        {
            Debug.DrawRay(transform.position, -transform.up * rayDist, Color.red);
            AccumulateObstalces(hit.point);
            return true;
        }
        else
        {
            ResetAvoid();
            return false;
        }
    }
    Vector3 Avoid()
    {
        if (avoidCount > 0)
        {
            return (avoidObst / avoidCount).normalized ;
        }

        return Vector3.zero;
    }
    public void AccumulateObstalces(Vector3 avoid)
    {
        avoidObst += transform.position - avoid;
        avoidCount++;

    }
    public void ResetAvoid()
    {
        avoidCount = 0;
        avoidObst *= 0;
    }
}
