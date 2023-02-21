using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New Inventory", menuName = "Inventory System/InventoryDavid")]
public class Inventory : MonoBehaviour
{
    public List<InventoryItem> inventory = new List<InventoryItem>();

    public int defaultWeight = 0;
    public int maxWeight = 100;

    Transform playerTrans;

    void Awake()
    {
        playerTrans = this.transform;
        inventory.Capacity = maxWeight;
    }
    public void GetItemType()
    {
        for (int i = 0; i < inventory.Count; i++)
        {
            
        }
    }
    public void GetItemWeight()
    {
        foreach (InventoryItem item in inventory)
        {
            defaultWeight += item.itemWeight;
            if (defaultWeight >= maxWeight)
            {
                playerTrans.GetComponent<Rigidbody>().useGravity = true;
            }
            else
            {
                playerTrans.GetComponent<Rigidbody>().useGravity = false;
            }
        }
    }

    // public void AddItem(TrashItem trash)
    // {
    //     for (int i = 0; i < inventory.Count; i++)
    //     {
    //         if (inventory[i])
    //         {
                
    //         }
            
    //     }
    // }
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        
    }
}
