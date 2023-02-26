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
    public void ClearWeight()
    {
        weight = 0;
    }
    public void BackPack()
    {
        for (int i = 0; i < inventoryContainer.Count; i++)
        {
            if (inventoryContainer[i].storedItemObj.Equals(ItemType.Backpack))
            {
                ItemObject backPackItem = inventoryContainer[i].storedItemObj;
                Debug.Log("I have the backpack. Its called" + backPackItem.GetType());
                // i wanted to add the backpackobjects weight increase, but havent figured it out yet
                inventoryContainer.Capacity += 100;
            }
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
    public ItemType GetItemType(ItemType itemType)
    {
        return itemType;
    }

}
