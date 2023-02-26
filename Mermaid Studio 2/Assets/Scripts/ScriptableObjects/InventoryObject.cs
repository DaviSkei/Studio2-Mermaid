using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName ="New Inventory", menuName = "Inventory System/Inventory")]

public class InventoryObject : ScriptableObject
// might have to change this back to scriptable object
{
    // List for items stored by a certain amount in a list called inventoryContainer
    public List<InventorySlot> inventoryContainer = new List<InventorySlot>();

    [SerializeField] int weight = 0;
    public int Weigth {get{return weight;}}
    [SerializeField] int maxWeight = 100;

    private void Awake()
    {
        inventoryContainer.Capacity = maxWeight;
    }
    public void AddItem(ItemObject _storedItemObJ, int _amount, int _weight)
    {
        weight += _weight;
        // check if inventory has item or not
        bool hasItem = false;

        // for each number in inventorycontainer, increase value of i
        for (int i = 0; i < inventoryContainer.Count; i++)
        {
            // if there are any number of stored items in inventorycontainer list, has item becomes true
            if (inventoryContainer[i].storedItemObj == _storedItemObJ)
            {
                inventoryContainer[i].AddAmount(_amount);
                hasItem = true;
                // break to stop continuosly going through for loop to check the boolean value
                break;
            }
        }
        // if inventory does not have the item, add a new slot for it
        if (!hasItem)
        {
            inventoryContainer.Add(new InventorySlot(_storedItemObJ, _amount));
        }
    }
    public void Trade(InventoryObject tradeFrom, InventoryObject tradeTo, InventorySlot tradeitem)
    {
        tradeFrom.inventoryContainer.Add(tradeitem);
        tradeTo.inventoryContainer.Add(tradeitem);
        Debug.Log("traded " + tradeitem);
    }
    public void ClearWeight()
    {
        weight = 0;
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
    // amount of slots
    public int amount;
    // setting values for when the inventory slots are created
    public InventorySlot(ItemObject _storedItemObJ, int _amount)
    {
        storedItemObj = _storedItemObJ;
        amount = _amount;

    }
    public void AddAmount(int value)
    {
        // the slot amount is equal to itself and value
        amount += value;
    }
    public void RemoveAmount(int value)
    {
        amount -= value;
    }
}
