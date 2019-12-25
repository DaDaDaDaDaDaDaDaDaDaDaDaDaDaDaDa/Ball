using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindAreaSettings : MonoBehaviour
{
    public float Strength;
    private List<Rigidbody> inAreaRigidBodies;

    private void Start()
    {
        inAreaRigidBodies = new List<Rigidbody>();
    }

    private void FixedUpdate()
    {
        foreach (var rb in inAreaRigidBodies)
        {
            if (rb == null)
                continue;
            rb.AddForce(Strength * transform.forward, ForceMode.Force);
        }
    }

    public void EnterWindArea(Rigidbody rigidbody)
    {
        inAreaRigidBodies.Add(rigidbody);
    }

    public void ExitWIndArea(Rigidbody rigidbody)
    {
        inAreaRigidBodies.Remove(rigidbody);
    }
}
