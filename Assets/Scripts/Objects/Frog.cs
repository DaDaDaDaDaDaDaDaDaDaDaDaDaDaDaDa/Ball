using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Frog : MonoBehaviour
{
    [SerializeField]
    private float upForceMin, upForceMax, sideForceMax;
    private Rigidbody rig;
    private void Start()
    {
        rig = GetComponent<Rigidbody>();
    }
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.layer != 9)
            return;

        rig.AddForce(Vector3.up * Random.Range(upForceMin, upForceMax), ForceMode.Impulse);
        rig.AddForce(Vector3.left * Random.Range(-sideForceMax, sideForceMax), ForceMode.Impulse);
        rig.AddForce(Vector3.forward * Random.Range(-sideForceMax, sideForceMax), ForceMode.Impulse);
    }
}
