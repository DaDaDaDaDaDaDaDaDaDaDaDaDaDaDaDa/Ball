using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class WindAreaTarget : MonoBehaviour
{
    private Rigidbody rigidbody;
    private void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("WindArea"))
        {
            other.gameObject.GetComponent<WindAreaSettings>().EnterWindArea(rigidbody);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("WindArea"))
        {
            other.gameObject.GetComponent<WindAreaSettings>().ExitWIndArea(rigidbody);
        }
    }
}
