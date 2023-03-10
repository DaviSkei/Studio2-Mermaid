using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [SerializeField] private float waveHeight = 0.0008f, waveSpeed = 0.05f, waveFrequency = 6f;
    // freq = ripple scale, speed = ripple speed, height = waveheight

    Material material;
    Texture2D displacementTexture;

    void Start()
    {
        SetVariables();
    }
    void SetVariables()
    {
        material = GetComponent<MeshRenderer>().material;
        displacementTexture = (Texture2D)material.GetTexture("_WaveDisplacement");

    }

    public float WaveHeight(Vector3 pos)
    {
        return transform.position.y + displacementTexture.GetPixelBilinear
        (pos.x * waveFrequency, pos.z * waveFrequency + Time.deltaTime * waveSpeed).g * waveHeight * transform.localScale.x;


        // (pos.x * waveFrequency, pos.z * waveFrequency + Time.deltaTime * (waveSpeed * -1f)).g * 
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
