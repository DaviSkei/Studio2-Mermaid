using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouyancySystem : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float depthB4Submerged = 1f;
    [SerializeField] private float displacementAmount = 3f;
    [SerializeField] int floaterCount = 1;
    [SerializeField] float waterDrag = 0.99f , waterAngularDrag = 0.5f;

    void Start()
    {
        
    }
    void FixedUpdate()
    {
        rb.AddForceAtPosition(Physics.gravity/floaterCount, transform.position , ForceMode.Acceleration);
        // waveheight equals the position of this transform on x axis
        float waveHeight = WaveManager.instance.GetWaveHeight(transform.position.x);

        if (transform.position.y < waveHeight)
        {
            float displacementMult = Mathf.Clamp01((waveHeight - transform.position.y) 
            / depthB4Submerged) * displacementAmount;

            rb.AddForceAtPosition(new Vector3(0f, Mathf.Abs(Physics.gravity.y)
            * displacementMult, 0f), transform.position, ForceMode.VelocityChange); 

            rb.AddForce(displacementMult * -rb.velocity * waterDrag
            * Time.fixedDeltaTime, ForceMode.VelocityChange);

            rb.AddTorque(displacementMult * -rb.angularVelocity * waterAngularDrag
             * Time.fixedDeltaTime, ForceMode.VelocityChange);
        }
    }
}
