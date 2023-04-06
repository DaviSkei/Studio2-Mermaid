using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoidBehaviour : MonoBehaviour
{
    private float startPos = 5;
    Vector3 constrainPos;
    // Start is called before the first frame update
    void Start()
    {
        Vector3 pos = new Vector3(Random.Range(-startPos, startPos),Random.Range(-startPos, startPos),Random.Range(-startPos, startPos));
        constrainPos = transform.parent.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
