using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class SwapMoveControl : MonoBehaviour
{
    [SerializeField] GameObject player;
    Movement playerScript;


    Movement fishScript;
    Movement defaultScript;

    [SerializeField] GameObject[] swappableBodies;

    [SerializeField] CinemachineFreeLook cam;

    private bool swapped;
    public bool Swapped {get{return swapped;}}
    int charDesignation;
    int num;

    // Start is called before the first frame update
    void Start()
    {
        playerScript = player.GetComponent<Movement>();
        swappableBodies = GameObject.FindGameObjectsWithTag("Fish");
        fishScript =  player.GetComponent<Movement>();
        defaultScript = fishScript;
    }

    // Update is called once per frame
    void Update()
    {
        foreach (GameObject body in swappableBodies)
            {
                body.GetComponent<Movement>();
                if (true)
                {
                    
                }
            }

        for (int i = 0; i < swappableBodies.Length; i++)
        {
            fishScript = swappableBodies[i].GetComponent<Movement>();

            if (fishScript.RayCastManager(swappableBodies[i]) && Input.GetKeyDown(KeyCode.Q))
            {
                num++;
                if (num == 1)
                {
                    charDesignation = i;
                    Swap();
                    swapped = true;
                    Debug.Log("swapped");
                }
                if (num == 2)
                {
                    SwapBack();
                }
                if (num == 3)
                {
                    num -= num;
                }
            }
        }
    }

    void Swap()
    {
        fishScript = swappableBodies[charDesignation].GetComponent<Movement>();
        cam.LookAt = fishScript.transform;
        cam.Follow = fishScript.transform;
    }
    void SwapBack()
    {
        fishScript = defaultScript;
        cam.LookAt = defaultScript.transform;
        cam.Follow = defaultScript.transform;
    }
    void OnApplicationQuit()
    {
        fishScript = defaultScript;
        cam.LookAt = defaultScript.transform;
        cam.Follow = defaultScript.transform;
    }
}
