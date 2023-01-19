using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animationStateController : MonoBehaviour
{
    Animator YBotAnimator;

    bool isMoving;


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

        if (Input.GetKey(KeyCode.Mouse1) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
         {
            YBotAnimator.SetBool("isSwimming", true);
         }
         else
         {
            YBotAnimator.SetBool("isSwimming", false);
         }
    }
    private bool CheckMovement(bool isMoving)
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        float forwardInput = Input.GetAxisRaw("Forward");

        Vector3 moveXY = new Vector3(horizontal, 0f, forwardInput);
        Vector3 moveY = new Vector3(0f, vertical, 0f);

        if (moveXY != Vector3.zero)
        {
            isMoving = true;
        }
        if (moveY != Vector3.zero)
        {
            isMoving = true;
        }
        if (moveXY == Vector3.zero || moveY == Vector3.zero)
        {
            isMoving = false;
        }
        return isMoving;

    }
}
