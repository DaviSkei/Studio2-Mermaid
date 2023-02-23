using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class DisplayInventory : MonoBehaviour
{
    [SerializeField] InventoryObject pcInventory;
    [SerializeField] TextMeshPro itemText;
    // Start is called before the first frame update
    void Start()
    {
        itemText = GetComponentInChildren<TextMeshPro>();
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < pcInventory.inventoryContainer.Count; i++)
        {
            // itemText.text = pcInventory.ToString
            //  sinusoidal but a triangular equation
        }
    }
}
