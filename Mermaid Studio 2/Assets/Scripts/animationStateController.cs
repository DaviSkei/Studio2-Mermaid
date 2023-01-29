using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animationStateController : MonoBehaviour
{
    Animator YBotAnimator;

    bool isSwimming;
    bool isSwimmingUp;

    void Start()
    {
        YBotAnimator = GetComponent<Animator>();

        isSwimming = YBotAnimator.GetBool("isSwimming");
        isSwimmingUp = YBotAnimator.GetBool("isSwimmingUp");
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S))
        {
            isSwimmingUp = true;
            YBotAnimator.SetBool("isSwimmingUp", true);
        }
        else
        {
            isSwimmingUp = false;
            YBotAnimator.SetBool("isSwimmingUp", false);
        }

        if (Input.GetKey(KeyCode.Mouse1)
         || Input.GetKey(KeyCode.Space) 
         || Input.GetKey(KeyCode.A) 
         || Input.GetKey(KeyCode.D))
         {
            isSwimming = true;
            YBotAnimator.SetBool("isSwimming", true);
         }
         else
         {
            isSwimming = false;
            YBotAnimator.SetBool("isSwimming", false);
         }
    }
}
