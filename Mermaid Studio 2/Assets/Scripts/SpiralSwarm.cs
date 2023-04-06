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


    void Start()
    {
        moveSpeed = Random.Range(minMoveSpeed, maxMoveSpeed);
        rotateSpeed = Random.Range(minRotSpeed, maxRotSpeed);
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
