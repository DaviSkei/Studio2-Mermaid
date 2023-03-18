using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EuiptmentLogic : MonoBehaviour
{
    [SerializeField] InventoryObject inventory;
    private GameObject player;
    private Animator playerAnimator;
    MovePlayer movePlayer;

    [SerializeField] GameObject playerBackPack;
    [SerializeField] GameObject playerKnife;
    [SerializeField] GameObject playerShovel;

    bool canUseKnife = false;
    bool canUseShovel = false;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<MovePlayer>().gameObject;
        movePlayer = player.GetComponent<MovePlayer>();
        playerAnimator = player.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckInventory();
    }
    void CheckInventory()
    {
        for (int i = 0; i < inventory.inventoryContainer.Count; i++)
        {
            if (inventory.inventoryContainer[i].storedItemObj.itemType == ItemType.Backpack)
            {
                playerBackPack.SetActive(true);
            }
            if (inventory.inventoryContainer[i].storedItemObj.itemType == ItemType.Knife)
            {
                canUseKnife = true;
            }
            if (inventory.inventoryContainer[i].storedItemObj.itemType == ItemType.Shovel)
            {
                canUseShovel = true;
            }
        }
    }
    void KnifeLogic()
    {
        bool mouseClick = Input.GetKey(KeyCode.Mouse0);
        bool equipKnife = Input.GetKeyDown(KeyCode.Alpha1);
        bool equipShovel = Input.GetKeyDown(KeyCode.Alpha2);

        int keyPressAmount = 0;
        if (canUseKnife && equipKnife)
        {
            playerKnife.SetActive(true);
            playerAnimator.SetBool("usingKnife", true);
            keyPressAmount = 1;

            if (movePlayer.Hit.transform.GetComponent<FishNet>() != null && mouseClick)
            {
                playerAnimator.SetBool("isCutting", true);
            }
            else
            {
                playerAnimator.SetBool("isCutting", false);
            }
        }
        if (keyPressAmount == 1 && equipKnife)
        {
            playerAnimator.SetBool("usingKnife", false);
            playerKnife.SetActive(false);
        }
    }
    void ShovelLogic()
    {
        bool mouseClick = Input.GetKey(KeyCode.Mouse0);
        bool equipShovel = Input.GetKeyDown(KeyCode.Alpha2);

        int keyPressAmount = 0;
        if (canUseShovel && equipShovel)
        {
            playerShovel.SetActive(true);
            playerAnimator.SetBool("usingKnife", true);
            keyPressAmount = 1;

            if (movePlayer.Hit.transform.GetComponent<FishNet>() != null && mouseClick)
            {
                playerAnimator.SetBool("isCutting", true);
            }
            else
            {
                playerAnimator.SetBool("isCutting", false);
            }
        }
        if (keyPressAmount == 1 && equipShovel)
        {
            playerShovel.SetActive(false);
            playerAnimator.SetBool("usingKnife", false);
        }
    }
}
