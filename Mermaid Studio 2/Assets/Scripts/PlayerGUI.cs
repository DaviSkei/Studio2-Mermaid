using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGUI : MonoBehaviour
{
    bool inRangeOfNpc;
    [SerializeField] LayerMask npcLayer;
    float sphereDistance = 5f;

    MovePlayer player;
    EuiptmentLogic equiptment;

    [Header("Add player related Game UI")]
    [SerializeField] GameObject speak2npcUi;
    [SerializeField] GameObject miniMap;
    [SerializeField] GameObject knifeReq;
    [SerializeField] GameObject shovelReq;
    [SerializeField] GameObject equipKnife;
    [SerializeField] GameObject equipShovel;
    [SerializeField] GameObject holdLMB;

    [SerializeField] GameObject pickUpTrash;

    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<MovePlayer>();
        equiptment = GetComponentInChildren<EuiptmentLogic>();
    }

    // Update is called once per frame
    void Update()
    {
        inRangeOfNpc = Physics.CheckSphere(transform.position, sphereDistance, npcLayer);

        ShowMap();
        NpcDialogue(); 
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            holdLMB.SetActive(false);
        }

        if (player.canPickUp)
        {
            pickUpTrash.SetActive(true);
        }
        else
        {
            pickUpTrash.SetActive(false);
        }
    }
    private void NpcDialogue()
    {
        if (inRangeOfNpc)
        {
            player.ShowCursor();
            speak2npcUi.SetActive(true);
        }
        else
        {
            player.HideCursor();
            speak2npcUi.SetActive(false);
        }
    }
    private void ShowMap()
    {
        if (Input.GetKey(KeyCode.M))
        {
            miniMap.SetActive(true);
        }
        else
        {
            miniMap.SetActive(false);
        }
    }
    void OnTriggerStay(Collider collider)
    {
        if (collider.transform.GetComponent<FishNet>() != null)
        {
            if (!equiptment.canUseKnife)
            {
                knifeReq.SetActive(true);
            }
            else
            {
                equipKnife.SetActive(true);
                
            }
            if (equiptment.UsingKnife)
            {
                equipKnife.SetActive(false);
                holdLMB.SetActive(true);
            }
        }

        if (collider.transform.GetComponent<GroundCrack>() != null)
        {
            if (!equiptment.canUseShovel)
            {
                shovelReq.SetActive(true);
            }
            else
            {
                equipShovel.SetActive(true);
                
            }
            if (equiptment.UsingShovel)
            {
                equipShovel.SetActive(false);
                holdLMB.SetActive(true);
            }
        }
    }
    void OnTriggerExit(Collider collider)
    {
        knifeReq.SetActive(false);
        shovelReq.SetActive(false);
        equipKnife.SetActive(false);
        equipShovel.SetActive(false);
    }
}
