using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    // store the item that this script is attached to
    public InventoryItem inventoryItem;

    void Start()
    {
        GetComponent<Rigidbody>();
    }
    void Update()
    {
        Physics.gravity = new Vector3 (0, -0.1f, 0);
    }
}
