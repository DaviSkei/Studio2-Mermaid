using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class SwapMoveControl : MonoBehaviour
{
    [SerializeField] GameObject player;
    Movement playerScript;

    GameObject defaultPlayer;
    Movement defaultScript;

    [SerializeField] GameObject[] swappableBodies;
    Movement fishScript;

    [SerializeField] CinemachineFreeLook cam;

    private bool swapped;
    public bool Swapped {get{return swapped;}}
    int charDesignation;

    // Start is called before the first frame update
    void Start()
    {
        playerScript = player.GetComponent<Movement>();
        swappableBodies = GameObject.FindGameObjectsWithTag("Fish");
        defaultPlayer = player;
        // fishScript =  player.GetComponent<Movement>();
        defaultScript = playerScript;

        cam.LookAt = defaultScript.transform;
        cam.Follow = defaultScript.transform;
    }

    // Update is called once per frame
    void Update()
    {
        // foreach (GameObject body in swappableBodies)
        //     {
        //         fishScript = body.GetComponent<Movement>();
        //     }

        for (int i = 0; i < swappableBodies.Length; i++)
        {
            fishScript = swappableBodies[i].GetComponent<Movement>();
            charDesignation = i;

            if (fishScript.RayCastManager(swappableBodies[i]) && Input.GetKeyDown(KeyCode.Q))
            {
                swapped = true;
            }
            if (fishScript.RayCastManager(player) && Input.GetKeyDown(KeyCode.Q))
            {
                swapped = false;
            }
            if (swapped)
            {
                Swap();
            }
            if (!swapped)
            {
                SwapBack();
            }
        }
    }

    void Swap()
    {
        Debug.Log("Swapped once");
        player = swappableBodies[charDesignation];
        playerScript =  player.GetComponent<Movement>();
        cam.LookAt = playerScript.transform;
        cam.Follow = playerScript.transform;
    }
    void SwapBack()
    {
        Debug.Log("Swapped twice");
        player = defaultPlayer;
        playerScript = defaultScript;
        cam.LookAt = defaultScript.transform;
        cam.Follow = defaultScript.transform;
    }
    void OnApplicationQuit()
    {
        player = defaultPlayer;
        playerScript = defaultScript;
        cam.LookAt = defaultScript.transform;
        cam.Follow = defaultScript.transform;
    }
}
