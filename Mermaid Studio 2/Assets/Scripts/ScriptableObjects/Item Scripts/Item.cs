using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    // store the item that this script is attached to
    [SerializeField] ItemObject itemObject;

    void Update()
    {
        Physics.gravity = new Vector3 (0, -0.5f, 0);
    }
    public ItemObject ItemObject()
    {
        return itemObject;
    }
}
