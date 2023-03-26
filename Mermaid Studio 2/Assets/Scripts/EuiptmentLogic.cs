using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EuiptmentLogic : MonoBehaviour
{
    [SerializeField] InventoryObject equiptmentInventory;
    private GameObject player;
    MovePlayer movePlayer;

    [SerializeField] GameObject playerBackPack;
    [SerializeField] GameObject playerKnife;
    [SerializeField] GameObject playerShovel;

    bool canUseKnife = false;
    bool canUseShovel = false;

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
    }
    void CheckInventory()
    {
        for (int i = 0; i < equiptmentInventory.inventoryContainer.Count; i++)
        {
            if (equiptmentInventory.inventoryContainer[i].storedItemObj.itemType == ItemType.Backpack)
            {
                playerBackPack.SetActive(true);
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
        bool unEquipKnife = Input.GetKeyUp(KeyCode.Alpha1);

        int keyPressAmount = 0;
        Debug.Log("knife amounts is " + keyPressAmount);
        if (canUseKnife && equipKnife)
        {
            // later, make it so the animation keyframes enable/disable knife and shovel
            playerKnife.SetActive(true);
            usingKnife = true;
            Debug.Log("knife enabled");

            keyPressAmount = 1;

            if (mouseClick && usingKnife)
            {
                isCutting = true;
            }
            else
            {
                isCutting = false;
            }
        }
        if (keyPressAmount == 1 && unEquipKnife)
        {
            usingKnife = false;
            playerKnife.SetActive(false);
            Debug.Log("knife disabled");
            keyPressAmount = 0;
        }
    }
    void ShovelLogic()
    {
        bool mouseClick = Input.GetKey(KeyCode.Mouse0);
        bool equipShovel = Input.GetKeyDown(KeyCode.Alpha2);
        bool unEquipShovel = Input.GetKeyUp(KeyCode.Alpha2);

        int keyPressAmount = 0;
        Debug.Log("shovel amounts is " + keyPressAmount);
        if (canUseShovel && equipShovel)
        {
            playerShovel.SetActive(true);
            usingShovel  = true;

            keyPressAmount = 1;
            Debug.Log("shovel enabled");

            if (mouseClick && usingShovel)
            {
                isDigging = true;
            }
            else
            {
                isDigging = false;
            }
        }
        if (keyPressAmount == 1 && unEquipShovel)
        {
            playerShovel.SetActive(false);
            usingShovel = false;
            Debug.Log("shovel disabled");
            keyPressAmount = 0;
        }
    }
}
