using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EuiptmentLogic : MonoBehaviour
{
    // this script should exist on an empty gameobject preferably under the player gameobject
    [SerializeField] InventoryObject equiptmentInventory;
    [SerializeField] InventoryObject playerInventory;
    private GameObject player;
    MovePlayer movePlayer;

    [SerializeField] GameObject playerBackPack;
    [SerializeField] GameObject playerKnife;
    [SerializeField] GameObject playerShovel;

    bool canUseKnife = false;
    bool canUseShovel = false;

    private bool hasBackpack;
    public bool HasBackpack {get{return hasBackpack;}}
    
    private bool usingKnife;
    public bool UsingKnife {get{return usingKnife;}}

    private bool isCutting;
    public bool IsCutting {get{return isCutting;}}

    private bool usingShovel;
    public bool UsingShovel {get{return usingShovel;}}

    private bool isDigging;
    public bool IsDigging {get{return isDigging;}}
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<MovePlayer>().gameObject;
        movePlayer = player.GetComponent<MovePlayer>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckInventory();
        KnifeLogic();
        ShovelLogic();
        BackpackLogic();
    }
    void CheckInventory()
    {
        for (int i = 0; i < equiptmentInventory.inventoryContainer.Count; i++)
        {
            if (equiptmentInventory.inventoryContainer[i].storedItemObj.itemType == ItemType.Backpack)
            {
                playerBackPack.SetActive(true);
                hasBackpack = true;
            }
            if (equiptmentInventory.inventoryContainer[i].storedItemObj.itemType == ItemType.Knife)
            {
                canUseKnife = true;
            }
            if (equiptmentInventory.inventoryContainer[i].storedItemObj.itemType == ItemType.Shovel)
            {
                canUseShovel = true;
            }
        }
    }
    void KnifeLogic()
    {
        bool mouseClick = Input.GetKey(KeyCode.Mouse0);
        bool equipKnife = Input.GetKeyDown(KeyCode.Alpha1);
        bool unEquipKnife = Input.GetKeyUp(KeyCode.Alpha3);


        if (canUseKnife && equipKnife)
        {
            // later, make it so the animation keyframes enable/disable knife and shovel
            playerKnife.SetActive(true);
            usingKnife = true;
        }
        if (mouseClick && usingKnife)
        {
            isCutting = true;
        }
        else
        {
            isCutting = false;
        }
        if (unEquipKnife)
        {
            usingKnife = false;
            playerKnife.SetActive(false);
        }
    }
    void ShovelLogic()
    {
        bool mouseClick = Input.GetKey(KeyCode.Mouse0);
        bool equipShovel = Input.GetKeyDown(KeyCode.Alpha2);
        bool unEquipShovel = Input.GetKeyUp(KeyCode.Alpha3);

        if (canUseShovel && equipShovel)
        {
            playerShovel.SetActive(true);
            usingShovel  = true;
        }
        if (mouseClick && usingShovel)
        {
            isDigging = true;
        }
        else
        {
            isDigging = false;
        }
        if (unEquipShovel)
        {
            playerShovel.SetActive(false);
            usingShovel = false;
        }
    }
    void BackpackLogic()
    {
        if (hasBackpack)
        {
            playerInventory.MaxWeight = 200;
        }
        if(playerInventory.TotalWeigth > playerInventory.MaxWeight)
        {
            movePlayer.RbPlayer.useGravity = true;
            movePlayer.GravityLogic();
        }
        else
        {
            movePlayer.RbPlayer.useGravity = false;
        }
    }
}
