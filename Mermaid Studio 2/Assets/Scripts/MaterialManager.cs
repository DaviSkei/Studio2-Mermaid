using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialManager : MonoBehaviour
{
    // This script needs to be attached to EVERY object that
    // is supposed to dissolve from trash proximity
    
    // Material properties
    MeshRenderer meshRenderer;
    SkinnedMeshRenderer skinnedMeshRend;
    Material[] materials;
    private string dissolveAmount = "_Dissolve_Amount";

    // Proximity variables
    private bool inRange;
    private float distance = 10f;
    private LayerMask layerMask = 1 << 7;
    // Set this to trash layer later

    // timer properties
    float visibleTimer = 2f;
    float invisTimer = -2f;
    float timer = -2f;
    private float slowedTime; 
    private float scaleTime;

    // scale properties
    Vector3 minScale;
    Vector3 maxScale;

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

        slowedTime = Time.deltaTime/6;
        maxScale = transform.localScale;
        minScale = maxScale/10f;
        scaleTime = Time.deltaTime/5;

    }
    private void Update()
    {
        VisibilityControl();
    }
    private void VisibilityControl()
    {
        inRange = Physics.CheckSphere(transform.position, distance, layerMask.value);

        Vector3 scaleDecrease = Vector3.Lerp(transform.localScale, minScale, scaleTime);
        Vector3 scaleIncrease = Vector3.Lerp(transform.localScale, maxScale, scaleTime);

        timer = Mathf.Clamp(timer, invisTimer, visibleTimer);
        // visToInvis = Mathf.Clamp(timer, visibleTimer, invisTimer);

        for( int i = 0; i < materials.Length; i++ )
        {
            if (!inRange)
            {
                timer -= slowedTime;
                materials[i].SetFloat(dissolveAmount, timer);

                transform.localScale = scaleIncrease;
            }
            else if (inRange)
            {
                timer += slowedTime;
                materials[i].SetFloat(dissolveAmount, timer);

                transform.localScale = scaleDecrease;
            }

        }

        // for (int i = 0; i < materials.Length; i++)
        // {
        //     if (!inRange)
        //     {
        //         visibleTimer -= slowedTime;
        //         materials[i].SetFloat(dissolveAmount, visibleTimer);

        //         invisTimer -= invisTimer;
        //         invisTimer = -3f;
        //         transform.localScale = scaleIncrease;
        //     }
        //     if (inRange)
        //     {  
        //         invisTimer += slowedTime;
        //         materials[i].SetFloat(dissolveAmount, invisTimer);

        //         visibleTimer -= visibleTimer;
        //         visibleTimer = 3f;
        //         transform.localScale = scaleDecrease;
        //     }
        // }     
    }
}

