using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    // store the item that this script is attached to
    public ItemObject item;
    public GameObject thisObj;

    void Start()
    {
        thisObj.GetComponent<Rigidbody>();
    }
    void Update()
    {
        Physics.gravity = new Vector3 (0, -0.1f, 0);
    }
}
