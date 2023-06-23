using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsMover : MonoBehaviour
{
    public float forceMultiplier;
    
    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    
    public void MoveWithExternalInput(float axisVal)
    {
        rb.AddForce(Vector3.right * (axisVal * forceMultiplier), ForceMode.Acceleration);
    }
}
