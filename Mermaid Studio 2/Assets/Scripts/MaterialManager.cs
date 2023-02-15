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

    public float timer = 1f;
    private float revealTime;

    public float timer2 = 0f;
    private float concealTime;

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

        revealTime = Time.deltaTime/25;
        concealTime = Time.deltaTime/20;
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
                timer2 -= concealTime;
                timer2 -= timer2;
                timer2 = 0f;

                timer -= revealTime;
                materials[i].SetFloat(dissolveAmount, timer);
            }
            else if (inRange)
            {
                timer -= revealTime;
                timer -= timer;
                timer = 1f;

                timer2 += concealTime;
                materials[i].SetFloat(dissolveAmount, timer2);
            }      
        }     
    }
}

