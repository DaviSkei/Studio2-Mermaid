using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName ="New Trash Object", menuName = "Inventory System/Items/Trash")]

public class TrashObject : ItemObject
{
    public void Awake()
    {
        // this scripts itemtype equals the Miscellaneous prefab, which is the default item type
        itemType = ItemType.Trash_Miscellaneous;
    }
}
