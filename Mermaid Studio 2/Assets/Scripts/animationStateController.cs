using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animationStateController : MonoBehaviour
{
    Animator YBotAnimator;
    MovePlayer player;
    EuiptmentLogic euiptmentLogic;
    

    string isSwimming = "isSwimming";
    string isSwimmingUp = "isSwimmingUp";

    string usingKnife = "usingKnife";
    string isCutting = "isCutting";
    string usingShovel = "usingShovel";
    string isDigging = "isDigging";

    void Start()
    {
        player = transform.parent.GetComponent<MovePlayer>();
        YBotAnimator = GetComponent<Animator>();
        euiptmentLogic = transform.parent.GetComponentInChildren<EuiptmentLogic>();
    }

    void Update()
    {
        YBotAnimator.SetBool(isSwimming, player.IsSwimming());
        YBotAnimator.SetBool(isSwimmingUp, player.IsSwimmingUp());
        YBotAnimator.SetBool(usingKnife, euiptmentLogic.UsingKnife);
        YBotAnimator.SetBool(isCutting, euiptmentLogic.IsCutting);
        YBotAnimator.SetBool(usingShovel, euiptmentLogic.UsingShovel);
        YBotAnimator.SetBool(isDigging, euiptmentLogic.IsDigging);
    }
}
