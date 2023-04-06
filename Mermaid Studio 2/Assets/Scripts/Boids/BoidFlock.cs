using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoidFlock : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 1f;
    private float startPos = 3;
    // Start is called before the first frame update
    void Start()
    {
        float xPos = transform.position.x;
        float yPos = transform.position.y;
        float zPos = transform.position.z;
        foreach (Transform boid in transform)
        {
            Vector3 pos = new Vector3(Random.Range(xPos, startPos),Random.Range(yPos, startPos),Random.Range(zPos, startPos));
            boid.transform.position += pos;
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(0,0, Time.deltaTime * moveSpeed);
    }
}
