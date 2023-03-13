using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [SerializeField] private float waveHeight = 0.0008f, waveSpeed = 0.05f, waveFrequency = 6f;
    Vector3 rippleOrigin;
    private float rippleDensity, rippleFrequency, rippleAmplitude;
    // freq = ripple scale, speed = ripple speed, height = waveheight

    Material material;
    Texture2D displacementTexture;

    
    void Start()
    {
        material = GetComponent<MeshRenderer>().material;
        displacementTexture = (Texture2D)material.GetTexture("_WaveDisplacement");
        rippleOrigin = material.GetVector("_Ripple_Origin");
        rippleDensity = material.GetFloat("_Ripple_Density");
        rippleFrequency = material.GetFloat("_Ripple_Frequency");
        rippleAmplitude = material.GetFloat("_Ripple_Amp");
        Debug.Log(rippleAmplitude);

    }
    private void Update()
    {
        rippleFrequency *= Time.deltaTime;
        rippleFrequency = Mathf.Clamp(rippleFrequency, 0.1f, 0.1f);
        Debug.Log(rippleFrequency);
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
