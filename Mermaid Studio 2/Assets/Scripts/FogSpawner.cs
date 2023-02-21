using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogSpawner : MonoBehaviour
{
    void OnTriggerStay(Collider collider)
    {
        RenderSettings.fog = false;
        Debug.Log("fog on");
    }
    void OnTriggerExit(Collider collider)
    {
        RenderSettings.fog = true;
        Debug.Log("fog on");
    }
}
