using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouyancySystem : MonoBehaviour
{
    // [SerializeField] private Rigidbody rb;
    // [SerializeField] private float depthB4Submerged = 1f;
    // [SerializeField] private float displacementAmount = 3f;
    // [SerializeField] int gravityMult = 1;
    // [SerializeField] float waterDrag = 0.99f , waterAngularDrag = 0.5f;

    // void Start()
    // {

    // }
    // void FixedUpdate()
    // {
    //     rb.AddForceAtPosition(Physics.gravity * gravityMult, transform.position , ForceMode.Acceleration);
    //     // waveheight equals the position of this transform on x axis
    //     float waveHeight = WaveManager.instance.GetWaveHeight(transform.position.x);

    //     if (transform.position.y < waveHeight+40)
    //     {
    //         Debug.Log(transform.position.y);
    //         float displacementMult = Mathf.Clamp01(((waveHeight+40) - transform.position.y) 
    //         / depthB4Submerged) * displacementAmount;

    //         rb.AddForceAtPosition(new Vector3(0f, Mathf.Abs(Physics.gravity.y)
    //         * displacementMult, 0f), transform.position, ForceMode.VelocityChange); 

    //         rb.AddForce(displacementMult * -rb.velocity * waterDrag
    //         * Time.fixedDeltaTime, ForceMode.VelocityChange);

    //         rb.AddTorque(displacementMult * -rb.angularVelocity * waterAngularDrag
    //         * Time.fixedDeltaTime, ForceMode.VelocityChange);
    //     }
    // }

    [SerializeField]
    private float waterDrag = 3f,
    waterAngularDrag = 1f,
    airDrag = 0f,
    airAngularDrag = 0.05f,
    floatingPower = 15f;

    WaveManager waveManager;
    [SerializeField] GameObject ocean;

    [SerializeField] private Transform[] floaterPoints;

    private Rigidbody rb;

    int floatersInWater;

    private bool isUnderwater;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Physics.gravity = new Vector3(0f, -500f, 0f);

        waveManager = ocean.GetComponent<WaveManager>();
    }

    void FixedUpdate()
    {
        floatersInWater = 0;

        for (int i = 0; i < floaterPoints.Length; i++)
        {
            float diff = floaterPoints[i].position.y - waveManager.WaveHeight(floaterPoints[i].position.x);

            if (diff < 0)
            {
                rb.AddForceAtPosition(Vector3.up * floatingPower * Mathf.Abs(diff), floaterPoints[i].position, ForceMode.Force);

                floatersInWater += 1;

                if (!isUnderwater)
                {
                    isUnderwater = true;
                    SwitchState(isUnderwater);
                }
            }
        }

        if (isUnderwater && floatersInWater == 0)
        {
            isUnderwater = false;
            SwitchState(isUnderwater);
        }
    }

    void SwitchState(bool underWater)
    {
        if (underWater)
        {
            rb.drag = waterDrag;
            rb.angularDrag = waterAngularDrag;
        }
        else
        {
            rb.drag = airDrag;
            rb.angularDrag = airAngularDrag;
        }
    }
}
