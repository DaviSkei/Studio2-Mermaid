using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemVariant
{
    // all the item types that will go into inventory. If more are needed, add here
    Equpitment,
    Trash_Miscellaneous

}
public abstract class InventoryItem : ScriptableObject
{
    // public GameObject itemPrefab;

    public ItemVariant itemVariant;

    public float itemWeight;
}
