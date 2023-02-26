using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    // all the item types that will go into inventory. If more are needed, add here
    Trash_Miscellaneous,
    Backpack,
    Knife,
    Rope,
    Shovel

}
public abstract class ItemObject : ScriptableObject
{
    // item UI
    public GameObject itemUI;
    // prefab of the gameobject
    public GameObject itemGameObj;
    // store the type of item
    public ItemType itemType;

    public int itemAmount = 1;

    public int itemWeight = 1;
}
