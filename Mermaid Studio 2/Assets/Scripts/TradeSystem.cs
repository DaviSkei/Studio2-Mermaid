using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This class should only exist on the player character
public class TradeSystem : MonoBehaviour
{
    [SerializeField] InventoryObject playerInventory;
    [SerializeField] InventoryObject npcInv;

    int tradedAmount = 0;

    // In the UI, select the item object you want to trade (should be the same as what is childed under the UI button)
    // and the npc will receive said item, by the amount that exists in the Players inventory, and by the weight
    public void Trade(ItemObject item)
    {
        for (int i = 0; i < playerInventory.inventoryContainer.Count; i++)
        {
            if (item.name == playerInventory.inventoryContainer[i].storedItemObj.name)
            {
                item = playerInventory.inventoryContainer[i].storedItemObj;
                int amount = playerInventory.inventoryContainer[i].amount;
                int weight = playerInventory.inventoryContainer[i].weight;

                npcInv.AddItem(item, amount, weight);
                Debug.Log("Item name = " + item);
                Debug.Log("item amount = " + amount);
                Debug.Log("item weight = " + weight);

                if (playerInventory.inventoryContainer[i].storedItemObj.itemType != ItemType.Backpack)
                {
                    playerInventory.inventoryContainer[i].ModifyAmount(amount);
                    playerInventory.ModifyWeight(weight);
                }
                tradedAmount++;
                Debug.Log("Amount of times traded = " + tradedAmount);
            }
        }
    }
}
