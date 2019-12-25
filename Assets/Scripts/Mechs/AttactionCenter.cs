using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttactionCenter : MonoBehaviour
{
    [SerializeField]
    private float force, attractRange;
    [SerializeField]
    private List<Rigidbody> rigidbodies;

    private Vector3 pose;

    private void FixedUpdate()
    {
        pose = transform.position;
        foreach (var rb in rigidbodies)
        {
            var targetPosition = rb.transform.position;
            if (Vector3.Magnitude(pose - targetPosition) > attractRange)
                continue;
            rb.AddForce((pose - targetPosition) * force, ForceMode.Force);
        }
    }
}
