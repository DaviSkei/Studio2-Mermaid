using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New Shovel Object", menuName = "Inventory System/Equiptment/Shovel")]
public class ShovelObject : ItemObject
{
    void Awake()
    {
        itemType = ItemType.Shovel;
    }
}
