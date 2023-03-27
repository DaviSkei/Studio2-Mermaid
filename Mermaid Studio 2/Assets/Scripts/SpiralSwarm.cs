using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiralSwarm : MonoBehaviour
{
    [SerializeField] float moveSpeed = 1f, rotateSpeed = 5f;

    // Update is called once per frame
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
