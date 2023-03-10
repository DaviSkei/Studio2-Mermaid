using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This class should only exist on the player character
public class TradeSystem : MonoBehaviour
{
    [SerializeField] InventoryObject playerInventory;
    [SerializeField] InventoryObject npcInv;

    int tradedAmount = 0;
    bool mouseDown;
    bool mouseUp;

    // In the UI, select the item object you want to trade (should be the same as what is childed under the UI button)
    // and the npc will receive said item, by the amount that exists in the Players inventory, and by the weight
    public void Trade(ItemObject item)
    {
        mouseDown = Input.GetKeyDown(KeyCode.Mouse0);
        mouseUp = Input.GetKeyUp(KeyCode.Mouse0);
        
        if (mouseDown && (playerInventory.Weigth >= 1))
        {
            for (int i = 0; i < playerInventory.inventoryContainer.Count; i++)
            {
                item = playerInventory.inventoryContainer[i].storedItemObj;

                npcInv.AddItem(item, item.itemAmount, item.itemWeight);
                
                // playerInventory.inventoryContainer[i].amount -= item.itemAmount;
                // playerInventory.Weigth -= item.itemWeight;

                // npcInv.AddItem(playerInventory.inventoryContainer[i].storedItemObj,
                // playerInventory.inventoryContainer[i].storedItemObj.itemAmount,
                // playerInventory.inventoryContainer[i].storedItemObj.itemWeight);
                if (playerInventory.inventoryContainer[i].storedItemObj.itemType != ItemType.Backpack)
                {
                    // playerInventory.ClearAmount();
                    // playerInventory.ClearWeight();
                }
                tradedAmount++;
            }

        }
        // if (mouseUp && (playerInventory.Weigth >= 1))
        // {
            
        //     playerInventory.ClearWeight();
        //     playerInventory.inventoryContainer.RemoveAt(0);
        // }
    }
}
