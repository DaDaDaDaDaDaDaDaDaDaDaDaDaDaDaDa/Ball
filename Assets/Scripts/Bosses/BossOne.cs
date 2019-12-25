using Assets.Scripts.MISC;
using System.Collections.Generic;
using UnityEngine;

public class BossOne : MonoBehaviour
{
    [SerializeField]
    private int hp;
    [SerializeField]
    private GameObject goalPrefab;
    [SerializeField]
    private List<GameObject> hpIndicators;
    [SerializeField]
    private List<FixedJoint> fixedJointsToBeDisabledUponDeath;
    [SerializeField]
    private List<Cannon> cannonsToBeDisabledUponDeath;

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
                foreach (var cannon in cannonsToBeDisabledUponDeath)
                {
                    cannon.enabled = false;
                }
            }
        }
    }
}
