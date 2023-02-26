using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;
using UnityEngine.UI;

public class DisplayInventory : MonoBehaviour
{
    [SerializeField] InventoryObject pcInventory;

    TextMeshProUGUI itemText;
    [SerializeField] ItemObject thisItemObject;

    Sprite sprite;
    
    void Start()
    {
        if (GetComponent<Image>().sprite != null)
        {
            sprite = GetComponent<Image>().sprite;
        }
        itemText = GetComponentInChildren<TextMeshProUGUI>();
    }

    void Update()
    {
        UpdateDisplay();
    }
    void UpdateDisplay()
    {
        for (int i = 0; i < pcInventory.inventoryContainer.Count; i++)
        {
            if (pcInventory.inventoryContainer[i].storedItemObj == thisItemObject)
            {
               itemText.text = pcInventory.inventoryContainer[i].amount.ToString();
            }
        }

    }
    // void CreateDisplay()
    // {
    //     for (int i = 0; i < pcInventory.inventoryContainer.Count; i++)
    //     {
    //         // instansiate the UI element that belongs to the itemType
    //         var obj = Instantiate(pcInventory.inventoryContainer[i].storedItemObj.itemUI,
    //         Vector3.zero, Quaternion.identity, transform);
    //         obj.GetComponent<RectTransform>().localPosition = GetPosition(i);
    //         obj.GetComponentInChildren<TextMeshProUGUI>().text = pcInventory.inventoryContainer[i].amount.ToString("n0");
    //     }

    // }
    // public Vector3 GetPosition(int i)
    // {
    //     return new Vector3(xStart + (xGap * (i%columnAmount)), (yStart + (-yGap * (i%columnAmount))), 0f);
    // }
}
