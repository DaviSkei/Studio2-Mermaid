using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    // this script is attached to the wave plane
    public static WaveManager instance;

    public float amplitude = 1f, waveLength = 2f, speed = 1f, offsetTime = 0f;

    private MeshRenderer meshRenderer;
    private Material material;

    private Vector3 rippleOrigin;
    private float rippleDensity, rippleFrequency, rippleAmplitude, waveSpeed, zAxis, waveAmplitude;

    private void Awake()
    {
        meshRenderer = GetComponentInChildren<MeshRenderer>();
        material = meshRenderer.material;

        if (instance == null)
        {
            instance = this;
        }
        else if(instance != this)
        {
            Destroy(this);
        }

        rippleOrigin = meshRenderer.material.GetVector("_Ripple_Origin");
        rippleDensity = meshRenderer.material.GetFloat("_Ripple_Density");
        rippleFrequency = meshRenderer.material.GetFloat("_Ripple_Frequency");
        rippleAmplitude = meshRenderer.material.GetFloat("_Ripple_Amp");
        waveSpeed = meshRenderer.material.GetFloat("_WaveSpeed");
        zAxis = meshRenderer.material.GetFloat("_Z_Wave");
        waveAmplitude = meshRenderer.material.GetFloat("_WaveAmplitude");
    }

    private void Update()
    {
        offsetTime += Time.deltaTime * rippleFrequency;
    }

    // method returns wave height of given x coordinate
    public float GetWaveHeight(float xCoordinate)
    {
        // return amplitude * Mathf.Sin(xCoordinate*waveLength + offsetTime);

        // xCoordinate = rippleOrigin.x;

        return rippleAmplitude * Mathf.Sin((xCoordinate +40) * rippleDensity + offsetTime);
    }
}
