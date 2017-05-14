using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CollisionAvoidance : MonoBehaviour {
    public float maxAcceleration = 15f;

    //public float agentRadius = 0.25f;

    private float characterRadius;

    private Rigidbody rb;
    private Rigidbody firstTarget;
    private float distance;
 
    private RaycastHit hitinfo = new RaycastHit();
    // Use this for initialization
    public void Start()
    {
        rb = GetComponent<Rigidbody>();
        characterRadius = SteeringBasics.getBoundingRadius(transform);

    }

    public Vector3 getSteering(ICollection<Rigidbody> targets)
    {
        Vector3 acceleration = Vector3.zero;

        /* 1. Find the target that the character will collide with first */

        /* The first collision time */
        float shortestTime = float.PositiveInfinity;

        /* The first target that will collide and other data that
		 * we will need and can avoid recalculating */
        firstTarget = null;
        //float firstMinSeparation = 0, firstDistance = 0;
        float firstMinSeparation = 0, firstDistance = 0, firstRadius = 0;
        Vector3 firstRelativePos = Vector3.zero, firstRelativeVel = Vector3.zero;

        foreach (Rigidbody r in targets)
        {
            distance = 0;

            hitinfo.point = Vector3.zero;
            //hitinfo = new RaycastHit();
            var ray = new Ray(rb.position, gameObject.GetComponent<Rigidbody>().velocity);
            if (!Physics.Raycast(ray, out hitinfo))
            {
                continue;
            }

            distance = hitinfo.distance;

            /* Calculate the time to collision */
            Vector3 relativePos = transform.position - hitinfo.point;
            Vector3 relativeVel = rb.velocity - r.velocity;
            
            float relativeSpeed = relativeVel.magnitude;

           

            if (relativeSpeed == 0)
            {
                continue;
            }
            
            float timeToCollision = -1 * Vector3.Dot(relativePos, relativeVel) / (relativeSpeed * relativeSpeed);

            /* Check if they will collide at all */
            Vector3 separation = relativePos + relativeVel * timeToCollision;
            float minSeparation = separation.magnitude;

            float targetRadius = SteeringBasics.getBoundingRadius(r.transform);

            if (minSeparation > characterRadius + targetRadius)
            //if (minSeparation > 2 * agentRadius)
            {
                continue;
            }

            /* Check if its the shortest */
            if (timeToCollision > 0 && timeToCollision < shortestTime)
            {
                shortestTime = timeToCollision;
                firstTarget = r;
                firstMinSeparation = minSeparation;
                firstDistance = distance;
                firstRelativePos = relativePos;
                firstRelativeVel = relativeVel;
                firstRadius = targetRadius;
            }
        }

        /* 2. Calculate the steering */

        /* If we have no target then exit */
        if (firstTarget == null)
        {
            return acceleration;
        }

        /* If we are going to collide with no separation or if we are already colliding then 
		 * steer based on current position */
        if (firstMinSeparation <= 0 || firstDistance < characterRadius + firstRadius)
        //if (firstMinSeparation <= 0 || firstDistance < 2 * agentRadius)
        {
            acceleration = transform.position - firstTarget.position;
        }
        /* Else calculate the future relative position */
        else
        {
            acceleration = firstRelativePos + firstRelativeVel * shortestTime;
        }

        /* Avoid the target */
        acceleration.Normalize();
        acceleration *= maxAcceleration;


        if (distance > 0)
        {
            //Debug.Log("collision avoidance vector values" +  acceleration / distance * distance);
            return acceleration / distance * distance;
        }

        return acceleration;
    }

    public void OnDrawGizmos()
    {
        if (firstTarget)
        {
            Gizmos.color = Color.magenta;
            Gizmos.DrawCube(firstTarget.position, Vector3.one);
        }

        if (!hitinfo.point.Equals(Vector3.zero))
        {
            Gizmos.DrawLine(rb.position, hitinfo.point);
        }
         
    }
}
