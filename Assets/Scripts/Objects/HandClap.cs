using Assets.Scripts.MISC;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandClap : MonoBehaviour
{
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Hand"))
        {
            AudioManager.Instance.PlaySound(Sounds.HandClap);
            VFXManager.Instance.GenerateVFXOnPosition(VFX.HandClap, other.contacts[0].point, 0.5f);
        }
    }
}
