using Assets.Scripts.MISC;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimedBomb : MonoBehaviour, IShootableObject, IDroppableObject
{
    [SerializeField]
    private float power, radius, upForce, timeInterval;
    [SerializeField]
    private Rigidbody rigidbody;
    private int emoteCounter = 1;
    private const int totalEmoteCount = 4;
    private float timer;
    private GameObject emoteObj;
    private bool dropped;

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(transform.position, radius);
    }

    private void FixedUpdate()
    {
        if (timer < 0 && dropped)
        {
            emoteObj = EmotesManager.Instance.GenerateEmoteOnPosition(GetEmotesEnumByCounter(), transform.position + Vector3.up * 0.5f);
            Destroy(emoteObj, timeInterval);
            timer += timeInterval;
            emoteCounter++;
            if (emoteCounter > totalEmoteCount)
            {
                Detonate();
            }
        }
        if (emoteObj != null)
        {
            emoteObj.transform.position = transform.position + Vector3.up * 0.5f;
        }
        timer -= Time.deltaTime;
    }

    private EmotesEnum GetEmotesEnumByCounter()
    {
        switch (emoteCounter)
        {
            case 1:
                return EmotesEnum.ThreeDots;
            case 2:
                return EmotesEnum.TwoDots;
            case 3:
                return EmotesEnum.OneDot;
            case 4:
                return EmotesEnum.Angry;
            default:
                return EmotesEnum.OneDot;
        }
    }

    private void Detonate()
    {
        var colliders = Physics.OverlapSphere(transform.position, radius);
        foreach (var hit in colliders)
        {
            if (hit.gameObject == gameObject)
                continue;

            var rig = hit.GetComponent<Rigidbody>();
            if (rig != null)
            {
                rig.AddExplosionForce(power, transform.position, radius, upForce, ForceMode.Impulse);
            }
        }

        VFXManager.Instance.GenerateVFXOnPosition(VFX.Explosion, transform.position, 1);
        AudioManager.Instance.PlaySound(Sounds.Bomb);
        Destroy(gameObject);
    }

    public void OnDrop()
    {
        rigidbody.useGravity = true;
        rigidbody.isKinematic = false;
        dropped = true;
    }

    public void OnShot()
    {
        OnDrop();
    }
}
