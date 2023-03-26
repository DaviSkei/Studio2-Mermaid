using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCrack : MonoBehaviour
{
    // attach script to ground cracks
    private float timer = 0f;
    void OnTriggerStay(Collider collider)
    {
        if (collider.transform.GetComponent<MovePlayer>() != null && 
        collider.transform.GetComponent<MovePlayer>().GetComponentInChildren<EuiptmentLogic>().IsDigging == true)
        {
            timer += Time.deltaTime;
            if (timer >= 5)
            {
                gameObject.SetActive(false);
            }
        }
    }
}
