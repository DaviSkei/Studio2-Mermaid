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
    private bool inTrigger;
    // public bool InTrigger {get{return inTrigger;} set { inTrigger = value;}}
    private float distance = 15f;
    private LayerMask layerMask = 1 << 9;
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

    // [Header("check off if you want to use scale effect")]
    // [SerializeField] bool changeScale;

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

        slowedTime = Time.deltaTime/3;
        maxScale = transform.localScale;
        minScale = maxScale/10f;
        scaleTime = Time.deltaTime/4;
    }
    private void Update()
    {
        VisibilityControl();
    }
    private void VisibilityControl()
    {
        inTrigger = Physics.CheckSphere(transform.position, distance, layerMask.value);

        // Vector3 scaleDecrease = Vector3.Lerp(transform.localScale, minScale, scaleTime);
        // Vector3 scaleIncrease = Vector3.Lerp(transform.localScale, maxScale, scaleTime);

        timer = Mathf.Clamp(timer, invisTimer, visibleTimer);

        for( int i = 0; i < materials.Length; i++ )
        {
            if (!inTrigger)
            {
                timer -= slowedTime;
                materials[i].SetFloat(dissolveAmount, timer);

                // if (changeScale)
                // {
                //     transform.localScale = scaleIncrease;
                // }
            }
            else if (inTrigger)
            {
                timer += slowedTime;
                materials[i].SetFloat(dissolveAmount, timer);

                // if (changeScale)
                // {
                //     transform.localScale = scaleDecrease;
                // }
            }
        }     
    }
}

