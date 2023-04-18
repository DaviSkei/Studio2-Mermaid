using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class SpiralSwarm : MonoBehaviour
{
    float moveSpeed, rotateSpeed, multRotSpeed, slowRotSpeed;

    [Range(1, 30)]
    [SerializeField] float minMoveSpeed = 8, maxMoveSpeed = 12;
    [Range(1, 30)]
    [SerializeField] float minRotSpeed = 8, maxRotSpeed = 12;

    RaycastHit hit;
    private float rayDist = 10f;
    LayerMask layerMask = 1<< 10;
    void Start()
    {
        moveSpeed = Random.Range(minMoveSpeed, maxMoveSpeed);
        rotateSpeed = Random.Range(minRotSpeed, maxRotSpeed);

        multRotSpeed = rotateSpeed * 6;
        slowRotSpeed = rotateSpeed * 0.1f;
    }
    void Update()
    {
        Move();
    }
    void Move()
    { 
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
        // forward ray
        if (Physics.Raycast(transform.position, transform.forward, out hit, rayDist, layerMask.value))
        {
            return true;
        }
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
