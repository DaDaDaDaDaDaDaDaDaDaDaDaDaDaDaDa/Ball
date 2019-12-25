using Assets.Scripts.MISC;
using UnityEngine;

public class BossFive : MonoBehaviour
{
    [SerializeField]
    private ContinuousRotating[] rotatingObjs;
    [SerializeField]
    private Transform rotatingParent, playerTransform;
    [SerializeField]
    private GameObject[] objsToBeDestroyedOnDeath, objsToBeDroppedOnDeath;
    [SerializeField]
    private GameObject goalPrefab, stunVFX;
    [SerializeField]
    private float rotateSpeed, timeInterval;
    private int hp = 2;
    private bool activated = true;
    private float timer;

    private void FixedUpdate()
    {
        timer += Time.fixedDeltaTime;
        if (timer >= timeInterval)
        {
            activated = !activated;

            if (activated)
                WakeUp();
            else
            {
                Sleep();
            }

            timer = 0;
        }
        if (!activated)
            return;

        var lookDirection = (playerTransform.position - Vector3.up * playerTransform.position.y) - (rotatingParent.position - Vector3.up * rotatingParent.position.y);
        var rotation = Quaternion.LookRotation(lookDirection);
        rotatingParent.rotation = Quaternion.Lerp(rotatingParent.rotation, rotation, rotateSpeed * Time.fixedDeltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            hp--;
            VFXManager.Instance.GenerateVFXOnPosition(VFX.HandClap, transform.position, 1);
            AudioManager.Instance.PlaySound(Sounds.HitMetal);
            if (hp <= 0)
            {
                var goal = Instantiate(goalPrefab);
                goal.transform.position = transform.position + Vector3.up * 1;
                foreach (var ro in rotatingObjs)
                {
                    ro.DisableRotation();
                    ro.GetComponent<MeshCollider>().enabled = false;
                    ro.gameObject.AddComponent<BoxCollider>();
                    ro.gameObject.AddComponent<Rigidbody>();
                }
                foreach (var obj in objsToBeDroppedOnDeath)
                {
                    obj.GetComponent<MeshCollider>().enabled = false;
                    obj.gameObject.AddComponent<BoxCollider>();
                    obj.gameObject.AddComponent<Rigidbody>();
                }
                foreach (var obj in objsToBeDestroyedOnDeath)
                {
                    Destroy(obj);
                }
                Destroy(gameObject);
            }

            //if attacked while sleeping, wake up immediately
            if(!activated)
            {
                WakeUp();
                activated = true;
                timer = 0;
            }
        }
    }

    private void Sleep()
    {
        foreach (var ro in rotatingObjs)
        {
            ro.DisableRotation();
        }
        foreach (var obj in objsToBeDestroyedOnDeath)
        {
            obj.SetActive(false);
        }
        stunVFX.SetActive(true);
    }

    private void WakeUp()
    {
        foreach (var ro in rotatingObjs)
        {
            ro.EnableRotation();
        }
        foreach (var obj in objsToBeDestroyedOnDeath)
        {
            obj.SetActive(true);
        }
        stunVFX.SetActive(false);
    }
}
