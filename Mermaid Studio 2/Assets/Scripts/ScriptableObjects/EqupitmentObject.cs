using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName ="New Equiptment Object", menuName = "Inventory System/Items/Equiptment")]

public class EqupitmentObject : ItemObject
{
    public void Awake()
    {
        // since equpitment items differ from default trash items, we make a new script spesifically for it
        itemType = ItemType.Equpitment;
    }
}
