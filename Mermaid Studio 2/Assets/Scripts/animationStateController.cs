using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animationStateController : MonoBehaviour
{
    Animator YBotAnimator;
    MovePlayer player;

    string isSwimming = "isSwimming";
    string isSwimmingUp = "isSwimmingUp";

    void Start()
    {
        player = transform.parent.GetComponent<MovePlayer>();
        YBotAnimator = GetComponent<Animator>();
    }

    void Update()
    {
        YBotAnimator.SetBool(isSwimming, player.IsSwimming());
        YBotAnimator.SetBool(isSwimmingUp, player.IsSwimmingUp());
    }
}
