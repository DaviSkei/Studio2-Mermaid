using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New Inventory", menuName = "Inventory System/InventoryDavid")]

public class Inventory : ScriptableObject
{
    public List<InventoryItem> inventory = new List<InventoryItem>();

    public float defaultWeight = 0;
    public float maxWeight = 100;

    void Awake()
    {
        inventory.Capacity = (int)maxWeight;
    }
    public void AddItem(InventoryItem item, float weight)
    {
        inventory.Add(item);
        weight = item.itemWeight;
        defaultWeight += weight;
    }
    public void Overencumbered()
    {
        if (defaultWeight >= maxWeight)
        {
            inventory.TrimExcess();
        }
    }
    public void ClearWeight()
    {
        defaultWeight = 0;
    }
}
