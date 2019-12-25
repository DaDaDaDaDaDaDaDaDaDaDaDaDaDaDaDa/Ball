using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FanMotor : MonoBehaviour
{
    [SerializeField]
    private float torque;
    private Rigidbody rig;
    // Start is called before the first frame update
    void Start()
    {
        rig = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rig.AddTorque(transform.up * torque, ForceMode.Force);
    }
}
