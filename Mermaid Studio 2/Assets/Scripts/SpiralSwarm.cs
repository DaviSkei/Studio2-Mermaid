using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiralSwarm : MonoBehaviour
{
    float moveSpeed, rotateSpeed;

    void Start()
    {
        moveSpeed = Random.Range(8, 12);
        rotateSpeed = Random.Range(80, 120);
    }
    void Update()
    {
        Move();
    }
    void Move()
    { 
        transform.position += transform.forward * moveSpeed * Time.deltaTime;
        transform.Rotate(Vector3.up * (rotateSpeed * Time.deltaTime));
    }
}
