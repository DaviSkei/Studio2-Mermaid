using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiverNPC : MonoBehaviour
{
    public InventoryObject diverInv;
    Animator npcAnimator;
    string isSwimming = "isSwimming";
    bool swimming;
    public bool Swimming {get{return swimming;} set{swimming = value;}}
    [SerializeField] float moveSpeed = 1, rotateSpeed = 1;
    // Start is called before the first frame update
    void Start()
    {
        npcAnimator = GetComponent<Animator>();
        swimming = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (swimming)
        {
            Move();
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
