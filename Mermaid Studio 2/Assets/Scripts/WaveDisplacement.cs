using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveDisplacement : MonoBehaviour
{
    private MeshFilter meshFilter;
    // Start is called before the first frame update
    void Start()
    {
        meshFilter = GetComponent<MeshFilter>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3[] vertecies = meshFilter.mesh.vertices;
        for (int i = 0; i < vertecies.Length; i++)
        {
            vertecies[i].y = WaveManager.instance.GetWaveHeight(transform.position.x + vertecies[i].x);
        }
        meshFilter.mesh.vertices = vertecies;
        meshFilter.mesh.RecalculateNormals();
    }
}
