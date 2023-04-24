using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DissolveController : MonoBehaviour
{
    private bool inRange;

    public bool InRange {get{return inRange;} set{ inRange = value;}}

    private float collDistance = 15f;
    private float rangeDistance = 8f;

    private LayerMask layerMask = 1 << 14;

    Collider[] colliders;

    MaterialManager materialManager;
    void Start()
    {

    }
    void Update()
    {
        colliders = Physics.OverlapSphere(transform.position, collDistance, layerMask, QueryTriggerInteraction.Collide);
        inRange = Physics.CheckSphere(transform.position, rangeDistance, layerMask, QueryTriggerInteraction.Collide);
        Debug.Log(inRange);

        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].transform.GetComponent<MaterialManager>() != null && inRange)
            {
                colliders[i].transform.GetComponent<MaterialManager>().InTrigger = true;
            }
            if (colliders[i].transform.GetComponent<MaterialManager>() != null && !inRange)
            {
                colliders[i].transform.GetComponent<MaterialManager>().InTrigger = false;
            }
        }
        // if (inRange)
        // {
        //     for (int i = 0; i < colliders.Length; i++)
        //     {
        //         if (colliders[i].transform.GetComponent<MaterialManager>() != null)
        //         {
        //             colliders[i].transform.GetComponent<MaterialManager>().InTrigger = true;
        //         }
        //     }
        // }
        // if (!inRange)
        // {
        //     for (int i = 0; i < colliders.Length; i++)
        //     {
        //         if (colliders[i].transform.GetComponent<MaterialManager>() != null)
        //         {
        //             colliders[i].transform.GetComponent<MaterialManager>().InTrigger = false;
        //         }
        //     }
        // }
    }

    // void Update()
    // {
    //     if (materialManager != null)
    //     {
    //         materialManager.InTrigger = inRange;
    //     }
    // }
    // void OnTriggerStay(Collider collider)
    // {
    //     if (collider.transform.GetComponent<MaterialManager>() != null)
    //     {
    //         inRange = true;
    //         materialManager = collider.transform.GetComponent<MaterialManager>();
    //         collider.transform.GetComponent<MaterialManager>().InTrigger = true;
    //     }
    // }
    // void OnTriggerExit(Collider collider)
    // {
    //     inRange = false;
    // }
}
