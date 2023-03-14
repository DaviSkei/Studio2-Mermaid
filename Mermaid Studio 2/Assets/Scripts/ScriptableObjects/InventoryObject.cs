using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName ="New Inventory", menuName = "Inventory System/Inventory")]

public class InventoryObject : ScriptableObject
{
    public List<InventorySlot> inventoryContainer = new List<InventorySlot>();

    [SerializeField] int totalWeight = 0;
    public int TotalWeigth { get { return totalWeight; } set { totalWeight = value; } }
    [SerializeField] int maxWeight = 100;

    private void Awake()
    {
        inventoryContainer.Capacity = maxWeight;
    }
    public void AddItem(ItemObject _storedItemObJ, int _amount, int _weight)
    {
        totalWeight += _weight;
        // check if inventory has item or not
        bool hasItem = false;

        // maybe need to swap this back to Count later
        for (int i = 0; i < inventoryContainer.Count; i++)
        {
            // if an item already exists in inventory, add its amount and weight instead
            if (inventoryContainer[i].storedItemObj == _storedItemObJ)
            {
                inventoryContainer[i].AddAmount(_amount);
                inventoryContainer[i].AddWeight(_weight);
                hasItem = true;
                // break to stop continuosly going through for loop to check the boolean value
                break;
            }
        }
        // if inventory does not have the item, add a new slot for it
        if (!hasItem)
        {
            inventoryContainer.Add(new InventorySlot(_storedItemObJ, _amount, _weight));
        }
    }
    public void ModifyTotalWeight(int _newWeight)
    {
        if (totalWeight >= 1)
        {
            totalWeight -= _newWeight;
        }
    }
    
    public void ClearTotalWeight()
    {
        totalWeight = 0;
    }
    public void ClearAmount()
    {
        for (int i = 0; i < inventoryContainer.Count; i++)
        {
            inventoryContainer[i].amount = 0;
        }
    }
}

[System.Serializable]
public class InventorySlot
{
    // variable for the item being stored in a slot
    public ItemObject storedItemObj;
    // amount of items in a slot
    public int amount;

    public int weight;
    
    // an inventory slot holds the item object, by an amount and a weight
    public InventorySlot(ItemObject _storedItemObJ, int _amount, int _weight)
    {
        storedItemObj = _storedItemObJ;
        amount = _amount;
        weight = _weight;
    }
    public void AddAmount(int value)
    {
        amount += value;
    }
    public void AddWeight(int value)
    {
        weight += value;
    }
    public void ModifyAmount(int _newAmount)
    {
        amount -= _newAmount;
    }
    public void ModifyWeight(int _newWeight)
    {
        weight -= _newWeight;
    }
    
}
