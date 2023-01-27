using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class SwapMoveControl : MonoBehaviour
{
    [SerializeField] GameObject player;
    MovePlayer playerScript;

    // GameObject defaultPlayer;
    // MovePlayer defaultScript;

    [SerializeField] GameObject[] swappableBodies;
    MoveFish[] fishScript;

    [SerializeField] CinemachineFreeLook cam;

    private bool swapped = false;
    public bool Swapped {get{return swapped;}}
    int charDesignation;

    // Start is called before the first frame update
    void Start()
    {
        playerScript = player.GetComponent<MovePlayer>();
        swappableBodies = GameObject.FindGameObjectsWithTag("Fish");
        // defaultPlayer = player;
        // fishScript =  player.GetComponent<Movement>();
        // defaultScript = playerScript;

        cam.LookAt = player.transform;
        cam.Follow = player.transform;
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < swappableBodies.Length; i++)
        {
            // fishScript[i] = swappableBodies[i].GetComponent<MoveFish>();
            charDesignation = i;

            if (playerScript.RayCastManager(swappableBodies[i]) && Input.GetKeyDown(KeyCode.Q))
            {
                swapped = true;
                Debug.Log("Swapped once");
                
                cam.LookAt = swappableBodies[i].transform;
                cam.Follow = swappableBodies[i].transform;
            }
            if (swapped && Input.GetKeyDown(KeyCode.F))
            {
                swapped = false;
                cam.LookAt = player.transform;
                cam.Follow = player.transform;
            }
        }
    }

    void Swap()
    {
        // Debug.Log("Swapped once");
        // player = swappableBodies[charDesignation];
        // playerScript =  player.GetComponent<Movement>();
        // cam.LookAt = playerScript.transform;
        // cam.Follow = playerScript.transform;
    }
    void SwapBack()
    {
        // Debug.Log("Swapped twice");
        // player = defaultPlayer;
        // playerScript = defaultScript;
        // cam.LookAt = defaultScript.transform;
        // cam.Follow = defaultScript.transform;
    }
    void OnApplicationQuit()
    {
        // player = defaultPlayer;
        // playerScript = defaultScript;
        // cam.LookAt = defaultScript.transform;
        // cam.Follow = defaultScript.transform;
    }
}
