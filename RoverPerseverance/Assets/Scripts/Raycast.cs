using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Raycast : MonoBehaviour
{
    public Rigidbody rb;

    public float travel;
    public float stiffness;
    public float damper;

    public float wheelRadius;

    float springVelocity;
    bool grounded;
    float lastLenght;
    RaycastHit hit;

    float springForce()
    {
        return stiffness * (travel - (hit.distance - wheelRadius));
    }

    float damperForce()
    {
        return damper * springVelocity;
    }

    private void FixedUpdate()
    {
        ShootRays();
    }

    void ShootRays()
    {
        lastLenght = travel - hit.distance - wheelRadius;
        if (Physics.Raycast(transform.position, -transform.up, out hit, travel + wheelRadius))
        {
            springVelocity= (travel - (hit.distance - wheelRadius) - lastLenght) / Time.fixedDeltaTime;
            ApplyForce();
            grounded = true;
            Debug.DrawRay(transform.position, -transform.up * hit.distance, Color.red);
        }
        else
        {
            grounded = false;
            Debug.DrawRay(transform.position, -transform.up * (travel + wheelRadius), Color.yellow);
        }
    }

    void ApplyForce()
    {
        rb.AddForceAtPosition(transform.up * (springForce() + damperForce()), transform.position, ForceMode.Force);
    }
}
