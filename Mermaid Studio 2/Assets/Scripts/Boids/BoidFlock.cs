using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoidFlock : MonoBehaviour
{
    [Range(1000, 10000)]
    [SerializeField] private float flockChangeFrequency = 5000;
    public float startPos { get; private set; }

    public Vector3 constrainPos { get; private set; }

    float xPos, yPos, zPos;

    private string constrainPoint = "ConstrainPoint";

    public Transform constrainTrans {get; private set;}

    void Start()
    {
        startPos = 3f;
        xPos = transform.position.x;
        yPos = transform.position.y;
        zPos = transform.position.z;
        foreach (Transform boid in transform)
        {
            Vector3 pos = new Vector3(Random.Range(-startPos, startPos), Random.Range(yPos, startPos), Random.Range(-startPos, startPos));
            boid.transform.position += pos;
        }
    }

    // Update is called once per frame
    void Update()
    {
        foreach (Transform boid in transform)
        {
            if (boid.name == constrainPoint)
            {
                constrainTrans = boid;
                if (Random.Range(0, flockChangeFrequency) < 50)
                {
                    constrainPos = new Vector3(Random.Range(-startPos, startPos), Random.Range(-startPos, startPos), Random.Range(-startPos, startPos));
                    constrainTrans.position = constrainPos;
                }
            }
        }
    }
}
