using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New Rope Object", menuName = "Inventory System/Equiptment/Rope")]
public class RopeObject : ItemObject
{
    public void Awake()
    {
        // this scripts itemtype equals the Miscellaneous prefab, which is the default item type
        itemType = ItemType.Rope;
    }
}
