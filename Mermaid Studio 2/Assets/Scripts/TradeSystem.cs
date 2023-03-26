using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TradeSystem : MonoBehaviour
{
    // this script should exist on an empty gameobject preferably under the player gameobject
    [SerializeField] InventoryObject playerInventory;
    [SerializeField] InventoryObject npcInv;
    [SerializeField] InventoryObject equiptmentInv;
    public GameObject equiptmentScreen;

    [SerializeField] ItemObject backpack;
    [SerializeField] ItemObject knife;
    [SerializeField] ItemObject shovel;

    int tradedAmount = 0;
    bool canTradeEquiptment;

    void Start()
    {
        npcInv.AddItem(backpack, 1, 1);
        npcInv.AddItem(knife, 1, 1);
        npcInv.AddItem(shovel, 1, 1);
    }
    
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
                // Debug.Log("Item name = " + item);
                // Debug.Log("item amount = " + amount);
                // Debug.Log("item weight = " + weight);

                playerInventory.inventoryContainer[i].ModifyAmount(amount);
                playerInventory.inventoryContainer[i].ModifyWeight(weight);
                playerInventory.ModifyTotalWeight(weight);

                tradedAmount++;
                Debug.Log("amount of times traded = "  + tradedAmount);
            }
            if (tradedAmount == 5 || tradedAmount == 10 || tradedAmount == 15)
            {
                equiptmentScreen.SetActive(true);
            }
            else
            {
                equiptmentScreen.SetActive(false);
            }
        }
    }
    public void DisableDisplay()
    {
        equiptmentScreen.SetActive(false);
    }
    public void GainEquiptment(ItemObject item)
    {
        for (int i = 0; i < npcInv.inventoryContainer.Count; i++)
        {
            if (item.name == npcInv.inventoryContainer[i].storedItemObj.name)
            {
                item = npcInv.inventoryContainer[i].storedItemObj;

                int amount = npcInv.inventoryContainer[i].amount;

                int weight = npcInv.inventoryContainer[i].weight;

                equiptmentInv.AddItem(item, amount, weight);
            }
        }
    }
    private void OnApplicationQuit()
    {
        playerInventory.inventoryContainer.Clear();
        playerInventory.ClearTotalWeight();
        playerInventory.ClearAmount();

        npcInv.inventoryContainer.Clear();
        npcInv.ClearTotalWeight();
        npcInv.ClearAmount();

        equiptmentInv.inventoryContainer.Clear();
        equiptmentInv.ClearTotalWeight();
        equiptmentInv.ClearAmount();
    }
}
