using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishNet : MonoBehaviour
{
    // attach script to fishnets
    private float timer = 0f;
    void OnTriggerStay(Collider collider)
    {
        if (collider.transform.GetComponent<MovePlayer>() != null && 
        collider.transform.GetComponent<MovePlayer>().GetComponentInChildren<EuiptmentLogic>().IsCutting == true)
        {
            timer += Time.deltaTime;
            if (timer >= 5)
            {
                gameObject.SetActive(false);
            }
        }
    }
}
