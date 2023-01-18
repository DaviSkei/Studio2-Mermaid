using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    // all the item types that will go into inventory. If more are needed, add here
    Equpitment,
    Trash_Miscellaneous

}
public abstract class ItemObject : ScriptableObject
{
    // prefab model for an itemtype
    public GameObject prefab;
    // store the type of item
    public ItemType itemType;
    // description size
    [TextArea(15, 20)]
    // store the item description
    public string description;
}
