using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DissolveController : MonoBehaviour
{
    // public bool hitByPlayer;
    // [SerializeField] List<MaterialManager> materialManager = new List<MaterialManager>();

    // void OnTriggerEnter(Collider collider)
    // {
    //     if (collider.transform.GetComponent<MaterialManager>() != null)
    //     {
    //         materialManager.Add(collider.transform.GetComponent<MaterialManager>());
    //         for (int i = 0; i < materialManager.Count; i++)
    //         {
    //             // collider.transform.GetComponent<MaterialManager>().InTrigger = true;
    //             materialManager[i].InTrigger = true;
    //         }
    //     }
    // }
    // void OnTriggerExit(Collider collider)
    // {
    //     for (int i = 0; i < materialManager.Count; i++)
    //     {
    //         materialManager[i].InTrigger = false;
    //         RemoveManager();
    //     }
    //     // if (collider.transform.GetComponent<MaterialManager>() != null)
    //     // {
    //     //     // collider.transform.GetComponent<MaterialManager>().InTrigger = false;
    //     // }
    // }
    // public void ChangeInTrigger()
    // {
    //     for (int i = 0; i < materialManager.Count; i++)
    //     {
    //         if (materialManager[i] != null)
    //         {
    //             materialManager[i].InTrigger = false;
    //         }
    //     }
    // }
    // private void RemoveManager()
    // {
    //     for (int i = 0; i < materialManager.Count; i++)
    //     {
    //         if (materialManager[i].InTrigger == false)
    //         {
    //             materialManager.Remove(materialManager[i]);
    //         } 
    //     }
    // }
}
