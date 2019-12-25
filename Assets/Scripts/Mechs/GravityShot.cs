using Assets.Scripts.MISC;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityShot : MonoBehaviour
{
    [SerializeField]
    private float power, radius, upForce, countDownTime;
    private float timer;

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(transform.position, radius);
    }

    private void FixedUpdate()
    {
        timer += Time.deltaTime;
        if(timer > countDownTime)
            Suck();
    }

    private void Suck()
    {
        VFXManager.Instance.GenerateVFXOnPosition(VFX.GravityShot, transform.position, 1);
        AudioManager.Instance.PlaySound(Sounds.GravityShot);
        var colliders = Physics.OverlapSphere(transform.position, radius);
        foreach (var hit in colliders)
        {
            if (hit.gameObject == gameObject)
                continue;

            var rig = hit.GetComponent<Rigidbody>();
            if (rig != null)
            {
                rig.AddExplosionForce(-power, transform.position, radius, upForce, ForceMode.Force);
            }
        }
        Destroy(gameObject);
    }
}
