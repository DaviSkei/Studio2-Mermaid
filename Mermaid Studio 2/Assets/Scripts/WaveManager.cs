using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    private float waveHeight = 0.0008f, waveSpeed = 0.05f, waveFrequency = 6f;

    Material material;
    Texture2D displacementTexture;

    void Start()
    {
        material = GetComponent<MeshRenderer>().material;
        displacementTexture = (Texture2D)material.GetTexture("_WaveDisplacement");
    }

    public float WaveHeight(Vector3 pos)
    {
        return displacementTexture.GetPixelBilinear
        (pos.x * waveFrequency, pos.z * waveFrequency + Time.deltaTime * (waveSpeed * -1f)).g * 
        (waveHeight * -0.5f) * transform.localScale.x;
    }
    
}
