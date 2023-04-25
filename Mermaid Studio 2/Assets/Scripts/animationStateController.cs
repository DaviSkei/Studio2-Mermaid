using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animationStateController : MonoBehaviour
{
    Animator animator;
    MovePlayer player;
    EuiptmentLogic euiptmentLogic;
    

    string isSwimming = "isSwimming";
    string isGrabbing = "isGrabbing";
    string isSwimmingUp = "isSwimmingUp";

    string usingKnife = "usingKnife";
    string isCutting = "isCutting";
    string usingShovel = "usingShovel";
    string isDigging = "isDigging";

    void Start()
    {
        player = transform.parent.GetComponent<MovePlayer>();
        animator = GetComponent<Animator>();
        euiptmentLogic = transform.parent.GetComponentInChildren<EuiptmentLogic>();
    }

    void Update()
    {
        animator.SetBool(isSwimming, player.IsSwimming());
        animator.SetBool(isGrabbing, player.isGrabbing);
        animator.SetBool(isCutting, euiptmentLogic.IsCutting);
        animator.SetBool(isDigging, euiptmentLogic.IsDigging);
    }
}
