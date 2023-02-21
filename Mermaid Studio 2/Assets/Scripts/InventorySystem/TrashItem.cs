using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New Trash Object", menuName = "Inventory System/Items/Trash Item")]

public class TrashItem : InventoryItem
{
    // Start is called before the first frame update
    void Awake()
    {
        itemVariant = ItemVariant.Trash_Miscellaneous;
    }
}
