using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName ="New Inventory", menuName = "Inventory System/Inventory")]

public class InventoryObject : ScriptableObject
// might have to change this back to scriptable object
{
    // List for items stored by a certain amount in a list called Container
    public List<InventorySlot> Container = new List<InventorySlot>();
    public void AddItem(ItemObject _storedItemObJ, int _amount)
    {
        // check if inventory has item or not
        bool hasItem = false;

        // for each number in container, increase value of i
        for (int i = 0; i < Container.Count; i++)
        {
            // if there are any number of stored items in container list, has item becomes true
            if (Container[i].storedItemObj == _storedItemObJ)
            {
                Container[i].AddAmount(_amount);
                hasItem = true;
                // break to stop continuosly going through for loop to check the boolean value
                break;
            }
        }
        // if inventory does not have the item, add a new slot for it
        if (!hasItem)
        {
            Container.Add(new InventorySlot(_storedItemObJ, _amount));
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

}
