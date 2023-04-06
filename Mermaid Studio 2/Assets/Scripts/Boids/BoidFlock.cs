using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoidFlock : MonoBehaviour
{
    [SerializeField] GameObject boidPrefab;
    [SerializeField] int boidAmount = 10;

    public GameObject[] boids {get; private set;}
    [Range(1000, 10000)]
    [SerializeField] private float flockChangeFrequency = 5000;
    public float startPos {get; private set;}

    public Vector3 newPos {get; private set;}

    float xPos, yPos, zPos;
    
    // Start is called before the first frame update
    void Start()
    {
        boids = new GameObject[boidAmount];
        startPos = 3f;
        xPos = transform.position.x;
        yPos = transform.position.y;
        zPos = transform.position.z;

        for (int i = 0; i < boids.Length; i++)
        {
            boids[i] = Instantiate(boidPrefab, transform.position, Quaternion.identity);

            Vector3 pos = new Vector3(Random.Range(xPos, startPos),Random.Range(yPos, startPos),Random.Range(zPos, startPos));
            boids[i].transform.position += pos;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Random.Range(0, flockChangeFrequency) < 50)
        {
            newPos = new Vector3(Random.Range(-startPos, startPos),Random.Range(-startPos, startPos),Random.Range(-startPos, startPos));
            transform.position += newPos;
        }
    }
}
