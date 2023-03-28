using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class DiverNPC : MonoBehaviour
{
    public InventoryObject diverInv;
    Animator npcAnimator;
    string isSwimming = "isSwimming";
    bool swimming;
    public bool Swimming {get{return swimming;} set{swimming = value;}}
    [SerializeField] float moveSpeed = 1, rotateSpeed = 1;

    Transform player;

    [SerializeField] CinemachineVirtualCamera npcCamera;
    // Start is called before the first frame update
    void Start()
    {
        npcAnimator = GetComponent<Animator>();
        swimming = true;
        player = FindObjectOfType<MovePlayer>().transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (swimming)
        {
            Move();
        }
        else
        {
            transform.LookAt(player);
        }
        npcAnimator.SetBool(isSwimming, swimming);
    }
    
    // move makes the npc swim in a wide circle
    void Move()
    { 
        transform.position += transform.forward * moveSpeed * Time.deltaTime;
        transform.Rotate(Vector3.up * (rotateSpeed * Time.deltaTime));
    }
}
