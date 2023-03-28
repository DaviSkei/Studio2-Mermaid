using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombineMeshes : MonoBehaviour
{
    MeshFilter[] meshFilters;
    CombineInstance[] combines;
    int i = 0;
    Vector3 objScale;
    // Start is called before the first frame update
    void Start()
    {
        meshFilters = GetComponentsInChildren<MeshFilter>();
        combines = new CombineInstance[meshFilters.Length];

        objScale = GetComponentInChildren<Transform>().localScale;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.K))
        {
            Combine();
        }
        Combine();
    }
    void Combine()
    {
        if (i < meshFilters.Length)
        {
            combines[i].mesh = meshFilters[i].sharedMesh;
            combines[i].transform = meshFilters[i].transform.localToWorldMatrix;
            meshFilters[i].gameObject.SetActive(false);
            i++;
        }
        MeshFilter meshFilter = transform.GetComponent<MeshFilter>();
        meshFilter.mesh = new Mesh();
        meshFilter.mesh.CombineMeshes(combines);
        GetComponent<MeshFilter>().sharedMesh = meshFilter.mesh;
        gameObject.SetActive(true);

        transform.localScale = objScale;
        transform.rotation = Quaternion.identity;
        transform.position = Vector3.zero;
    }
}
