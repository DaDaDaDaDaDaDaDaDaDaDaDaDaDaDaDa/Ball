using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    [SerializeField]
    private Teleporter targetTeleporter;
    [SerializeField]
    private float coolDown;
    private float timer;
    private bool used;
    
    private void FixedUpdate()
    {
        if (!used)
            return;

        timer += Time.deltaTime;
        if (timer >= coolDown)
        {
            timer = 0;
            used = false;
        }

    }
    private void OnTriggerEnter(Collider other)
    {
        if (used || targetTeleporter == null)
            return;

        targetTeleporter.OnReceivedTeleportedObject(other.transform);
        used = true;
    }

    public void OnReceivedTeleportedObject(Transform objTransform)
    {
        if (used)
            return;

        objTransform.position = transform.position;
        used = true;
    }

    public void AssignTarget(Teleporter target)
    {
        targetTeleporter = target;
    }
}
