using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New BackPack Object", menuName = "Inventory System/Equiptment/Backpack")]

public class BackPackObject : ItemObject
{
    public int containerIncrease {get{return containerIncrease;} private set {containerIncrease = 100;}}

    void Awake()
    {
        itemType = ItemType.Backpack;
    }
}
