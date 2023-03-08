using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public static WaveManager instance;

    public float amplitude =1f, lenght = 2f, speed = 1f, offset = 0f;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if(instance != this)
        {
            Destroy(this);
        }
    }

    private void Update()
    {
        offset += Time.deltaTime * speed;
    }

    // method returns wave height of given x coordinate
    public float GetWaveHeight(float xCoordinate)
    {
        // x coord divided by length, add offset, passed through sin function
        // the result is the sine multiplied by the amplitude

        // in shader, i think itll will be similar
        return amplitude * Mathf.Sin(xCoordinate/lenght + offset);
    }
}
