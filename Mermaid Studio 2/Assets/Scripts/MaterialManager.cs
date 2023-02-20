using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialManager : MonoBehaviour
{
    // This script needs to be attached to EVERY object that
    // is supposed to dissolve from trash proximity
    MeshRenderer meshRenderer;

    SkinnedMeshRenderer skinnedMeshRend;
    Material[] materials;
    private string dissolveAmount = "_Dissolve_Amount";

    bool inRange;
    private float distance = 10f;
    // might have to change distance later
    private LayerMask layerMask = 1 << 7;
    // SET LAYERMASK TO A NUMBER INSTEAD SO WE DONT HAVE TO DO THIS FOR EVERY SINGLE FUCKING TRASH SCRIPT
    // set it equal to trash number later

    public float visibleTimer = 3f;

    public float invisTimer = -3f;

    private float slowedTime;

    // Start is called before the first frame update
    void Start()
    {
        if (GetComponent<MeshRenderer>() != null)
        {
            meshRenderer = GetComponent<MeshRenderer>();
            materials = meshRenderer.materials;
        }
        if (GetComponent<SkinnedMeshRenderer>() != null)
        {
            skinnedMeshRend = GetComponent<SkinnedMeshRenderer>();
            materials = skinnedMeshRend.materials;
        }

        slowedTime = Time.deltaTime/15;
    }
    private void Update()
    {
        VisibilityControl();
    }
    private void VisibilityControl()
    {
        inRange = Physics.CheckSphere(transform.position, distance, layerMask.value);

        for (int i = 0; i < materials.Length; i++)
        {
            if (!inRange)
            {
                visibleTimer -= slowedTime;
                materials[i].SetFloat(dissolveAmount, visibleTimer);

                invisTimer -= invisTimer;
                invisTimer = -3f;

            }
            if (inRange)
            {  
                invisTimer += slowedTime;
                materials[i].SetFloat(dissolveAmount, invisTimer);

                visibleTimer -= visibleTimer;
                visibleTimer = 3f;
            }
        }     
    }
}

