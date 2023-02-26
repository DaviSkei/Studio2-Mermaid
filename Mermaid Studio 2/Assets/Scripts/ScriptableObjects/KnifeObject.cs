using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New Knife Object", menuName = "Inventory System/Equiptment/Knife")]
public class KnifeObject : ItemObject
{
    public void Awake()
    {
        // this scripts itemtype equals the Miscellaneous prefab, which is the default item type
        itemType = ItemType.Knife;
    }
}
