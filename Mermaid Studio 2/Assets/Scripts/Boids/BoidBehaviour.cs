using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoidBehaviour : MonoBehaviour
{
    [Range(0.1f, 10f)]
    [SerializeField] private float moveSpeed = 3f, rotateSpeed = 2f;
    Transform flock;
    BoidFlock boidFlock;
    Vector3 groupCenter = Vector3.zero;
    Vector3 groupAvoidance = Vector3.zero;
    float distance;
    int groupSize = 0;
    float neighbourDist = 3f;

    bool isTurning = false;
    
    private string constrainPoint = "ConstrainPoint";
    // Start is called before the first frame update
    void Start()
    {
        flock = transform.parent;
        boidFlock = flock.GetComponent<BoidFlock>();
    }

    void Update()
    {
        foreach (Transform boid in flock)
        {
            if (boid.name != constrainPoint)
            {
                if (Vector3.Distance(boid.transform.position, Vector3.zero) >= boidFlock.startPos)
                {
                    isTurning = true;
                    Debug.Log("im turning");
                }
                else
                {
                    isTurning = false;
                    Debug.Log("no turning");
                }
            }
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
        Vector3 constrainPos = boidFlock.constrainPos;

        foreach (Transform boid in flock)
        {
            if (boid != this.transform && boid.transform.name != constrainPoint)
            {
                distance = Vector3.Distance(boid.transform.position, this.transform.position);
                if (distance <= neighbourDist)
                {
                    groupCenter += boid.transform.position;
                    groupSize++;
                    Debug.Log("im grouping");
                    if (distance < 1f)
                    {
                        groupAvoidance += (this.transform.position - boid.transform.position);
                        Debug.Log("im not grouping");
                    }
                }
            }
        }

        if (groupSize > 0)
        {
            groupCenter = groupCenter/groupSize + (constrainPos - this.transform.position);

            Vector3 direction = (groupCenter + groupAvoidance) - transform.position;
            if (direction != Vector3.zero)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), rotateSpeed * Time.deltaTime);
            }
        }
    }
}
