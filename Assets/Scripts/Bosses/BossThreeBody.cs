using Assets.Scripts.MISC;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossThreeBody : MonoBehaviour
{
    [SerializeField]
    private BossThree bossThree;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            var validHit = bossThree.OnBodyHit();
            if (validHit)
            {
                VFXManager.Instance.GenerateVFXOnPosition(VFX.HandClap, transform.position, 1);
                AudioManager.Instance.PlaySound(Sounds.HitMetal);
            }
        }
    }
}
