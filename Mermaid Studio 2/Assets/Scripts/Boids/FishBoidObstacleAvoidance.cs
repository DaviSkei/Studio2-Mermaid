using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class FishBoidObstacleAvoidance : MonoBehaviour
{

    private FishBoid boids;
    private Transform boid;

    private JellyfishBoid jellyBoid;

    [SerializeField] LayerMask avoidMask;

    private void Start()
    {
        boid = transform.parent;
        if (boid.GetComponent<FishBoid>() != null)
        {
            boids = boid.GetComponent<FishBoid>();
        }
        if(boid.GetComponent<JellyfishBoid>() != null)
        {
            jellyBoid = boid.GetComponent<JellyfishBoid>();
        }
    }


    private void Update()
    {
        bool didHit = false;
        RaycastHit hit;
        // Does the ray intersect any objects in the layer mask
        if (boids)
        {
            if (Physics.Raycast(boid.position, boid.forward, out hit, 10, avoidMask))
            {
                Debug.DrawRay(boid.position, boid.forward * hit.distance, Color.red);
    
                boids.accumAvoid(hit.point);
    
                didHit = true;
            }
            if (Physics.Raycast(boid.position, -boid.up, out hit, 10, avoidMask))
            {
                Debug.DrawRay(boid.position, -boid.up * hit.distance, Color.red);
    
                boids.accumAvoid(hit.point);
    
                didHit = true;
            }
            if (Physics.Raycast(boid.position, boid.up, out hit, 10, avoidMask))
            {
                Debug.DrawRay(boid.position, boid.up * hit.distance, Color.red);
    
                boids.accumAvoid(hit.point);
    
                didHit = true;
            }
            if (Physics.Raycast(boid.position, boid.right, out hit, 10, avoidMask))
            {
                Debug.DrawRay(boid.position, boid.right * hit.distance, Color.red);
    
                boids.accumAvoid(hit.point);
    
                didHit = true;
            }
            if (Physics.Raycast(boid.position, -boid.right, out hit, 10, avoidMask))
            {
                Debug.DrawRay(boid.position, -boid.right * hit.distance, Color.red);
    
                boids.accumAvoid(hit.point);
    
                didHit = true;
            }
    
            if (!didHit)
            {
                boids.resetAvoid();
            }
        }
        if (jellyBoid)
        {
            if (Physics.Raycast(boid.position, boid.forward, out hit, 10, avoidMask))
            {
                Debug.DrawRay(boid.position, boid.forward * hit.distance, Color.red);
    
                jellyBoid.accumAvoid(hit.point);
    
                didHit = true;
            }
            if (Physics.Raycast(boid.position, -boid.up, out hit, 10, avoidMask))
            {
                Debug.DrawRay(boid.position, -boid.up * hit.distance, Color.red);
    
                jellyBoid.accumAvoid(hit.point);
    
                didHit = true;
            }
            if (Physics.Raycast(boid.position, boid.up, out hit, 10, avoidMask))
            {
                Debug.DrawRay(boid.position, boid.up * hit.distance, Color.red);
    
                jellyBoid.accumAvoid(hit.point);
    
                didHit = true;
            }
            if (Physics.Raycast(boid.position, boid.right, out hit, 10, avoidMask))
            {
                Debug.DrawRay(boid.position, boid.right * hit.distance, Color.red);
    
                jellyBoid.accumAvoid(hit.point);
    
                didHit = true;
            }
            if (Physics.Raycast(boid.position, -boid.right, out hit, 10, avoidMask))
            {
                Debug.DrawRay(boid.position, -boid.right * hit.distance, Color.red);
    
                jellyBoid.accumAvoid(hit.point);
    
                didHit = true;
            }
    
            if (!didHit)
            {
               jellyBoid.resetAvoid();
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (boid.GetComponent<FishBoid>() != null)
        {
            boids.resetAvoid();
        }
        if(boid.GetComponent<JellyfishBoid>() != null)
        {
            jellyBoid.resetAvoid();
        }
    }
}

