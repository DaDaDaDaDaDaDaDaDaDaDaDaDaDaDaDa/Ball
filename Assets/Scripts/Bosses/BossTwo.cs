using Assets.Scripts.MISC;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTwo : MonoBehaviour
{
    [SerializeField]
    private Rigidbody[] footRigidbodys;
    [SerializeField]
    private int hp;
    [SerializeField]
    private GameObject goalPrefab, eggPrefab;
    [SerializeField]
    private List<GameObject> hpIndicators;
    [SerializeField]
    private List<FixedJoint> fixedJointsToBeDisabledUponDeath;
    [SerializeField]
    private float timerInterval, upForce, sideForceRange;

    private float timer;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            hp--;
            Destroy(hpIndicators[hpIndicators.Count - 1]);
            hpIndicators.RemoveAt(hpIndicators.Count - 1);
            VFXManager.Instance.GenerateVFXOnPosition(VFX.HandClap, transform.position, 1);
            AudioManager.Instance.PlaySound(Sounds.HitMetal);
            if (hp <= 0)
            {
                var goal = Instantiate(goalPrefab);
                goal.transform.position = transform.position + Vector3.up * 1;
                Destroy(gameObject);
                foreach (var joint in fixedJointsToBeDisabledUponDeath)
                {
                    joint.breakForce = 0;
                }
            }
        }
    }

    private void FixedUpdate()
    {
        timer -= Time.deltaTime;
        if (timer < 0)
        {
            Jump();
            timer = timerInterval;
        }
    }

    private void Jump()
    {
        var egg = Instantiate(eggPrefab);
        egg.transform.position = new Vector3(transform.position.x, 0.2f, transform.position.z);
        Destroy(egg, 2);
        foreach (var rigidbody in footRigidbodys)
        {
            rigidbody.AddForce(new Vector3(Random.Range(-sideForceRange, sideForceRange), upForce, Random.Range(-sideForceRange, sideForceRange)), ForceMode.Impulse);
        }
    }
}
