using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashItem : InventoryItem
{

    // Start is called before the first frame update
    void Awake()
    {
        itemVariant = ItemVariant.Trash_Miscellaneous;
        itemPrefab = this.gameObject;
        itemWeight = 1;
    }
}
