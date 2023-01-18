using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animationStateController : MonoBehaviour
{
    Animator YBotAnimator;


    // Start is called before the first frame update
    void Start()
    {
        YBotAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S))
        {
            YBotAnimator.SetBool("isSwimmingUp", true);
        }
        else
        {
            YBotAnimator.SetBool("isSwimmingUp", false);
        }

        if (Input.GetKey(KeyCode.Mouse1)  
         || Input.GetKey(KeyCode.A) 
         || Input.GetKey(KeyCode.D))
         {
            YBotAnimator.SetBool("isSwimming", true);
         }
         else
         {
            YBotAnimator.SetBool("isSwimming", false);
         }
    }
}
