using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoidBehaviour : MonoBehaviour
{
    [Range(1, 10)]
    [SerializeField] private float moveSpeed = 3f, rotateSpeed = 2f;
    BoidFlock boidFlock;
    Vector3 averageheading, averagePos;
    float neighbourDist = 5f;

    bool isTurning = false;
    
    // Start is called before the first frame update
    void Start()
    {
        boidFlock = FindObjectOfType<BoidFlock>();
    }

    void Update()
    {
        if (Vector3.Distance(transform.position, Vector3.zero) >= boidFlock.startPos)
        {
            isTurning = true;
        }
        else
        {
            isTurning = false;
        }
        if (isTurning)
        {
            Vector3 direction = Vector3.zero - transform.position;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), rotateSpeed * Time.deltaTime);
        }
        else
        {
            if (Random.Range(0,5) < 1)
            {
                ApplyRules();
            }
        }
        transform.Translate(0,0, Time.deltaTime * moveSpeed);
    }
    void ApplyRules()
    {
        Vector3 groupCenter = Vector3.zero;
        Vector3 groupAvoidance = Vector3.zero;

        Vector3 changePos = boidFlock.newPos;

        float distance;
        int groupSize = 0;

        foreach (GameObject boid in boidFlock.boids)
        {
            if (boid != this.transform)
            {
                distance = Vector3.Distance(boid.transform.position, this.transform.position);
                if (distance <= neighbourDist)
                {
                    groupCenter += boid.transform.position;
                    groupSize++;
                    if (distance < 3f)
                    {
                        groupAvoidance += this.transform.position - boid.transform.position;
                    }
                }
            }
        }

        if (groupSize > 0)
        {
            groupCenter = groupCenter/groupSize + (changePos -this.transform.position);

            Vector3 direction = (groupCenter + groupAvoidance) - transform.position;
            if (direction != Vector3.zero)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), rotateSpeed * Time.deltaTime);
            }
        }
    }
}
