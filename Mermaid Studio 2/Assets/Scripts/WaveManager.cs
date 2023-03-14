using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    Vector3 rippleOrigin;
    private float rippleDensity, rippleFrequency, rippleAmplitude;

    Material material;
    
    void Start()
    {
        material = GetComponent<MeshRenderer>().material;

        rippleOrigin = material.GetVector("_Ripple_Origin");
        rippleDensity = material.GetFloat("_Ripple_Density");
        rippleFrequency = material.GetFloat("_Ripple_Frequency");
        rippleAmplitude = material.GetFloat("_Ripple_Amp");

        rippleFrequency *= Time.deltaTime;
    }
    private void Update()
    {
        rippleFrequency += Time.deltaTime;
    }

    public float WaveHeight(float xPos)
    {
        return rippleAmplitude * Mathf.Sin(xPos * rippleDensity) + rippleFrequency;

        // return (pos.x * waveFrequency, pos.z * waveFrequency + Time.deltaTime * (waveSpeed * -1f)).g * 
        // (waveHeight * -0.5f) * transform.localScale.x;
    }
    // void OnValidate()
    // {
    //     if (!material)
    //     {
    //         SetVariables();

    //         UpdateMaterial();
    //     }
    // }

    // void UpdateMaterial()
    // {
    //     material.SetFloat("_Ripple_Speed", waveSpeed);
    //     material.SetFloat("_Riple_Scale", waveFrequency);
    //     material.SetFloat("_WaveHeight", waveHeight);
    // } 
}
