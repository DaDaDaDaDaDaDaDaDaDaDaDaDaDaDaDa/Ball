using Assets.Scripts.MISC;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossThree : MonoBehaviour
{
    [SerializeField]
    private int hp;
    [SerializeField]
    private GameObject goalPrefab;
    [SerializeField]
    private List<SpringJoint> springJointsOnBalls;
    [SerializeField]
    private float timerInterval;
    [SerializeField]
    private Transform coilTransform;
    [SerializeField]
    private List<Material> runeMats;
    [SerializeField]
    private List<LineRendering> lineRenderings;
    [SerializeField]
    private List<Rigidbody> toBeEnabledUponDeath;

    private float timer;
    private bool released;
    private float orginalSpring;
    private int disabledRuneCounter;

    public bool OnBodyHit()
    {
        if (released)
            return false;

        runeMats[disabledRuneCounter].DisableKeyword("_EMISSION");
        disabledRuneCounter++;
        if (disabledRuneCounter >= runeMats.Count)
        {
            ReleaseBalls();
        }
        return true;
    }

    private void Start()
    {
        orginalSpring = springJointsOnBalls[0].spring;
        timer = timerInterval;
        foreach (var mat in runeMats)
        {
            mat.EnableKeyword("_EMISSION");
        }
    }
    private void FixedUpdate()
    {
        if (!released)
            return;
        
        timer -= Time.deltaTime;
        if (timer < 0)
        {
            CollectBalls();
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!released)
            return;

        if (collision.gameObject.CompareTag("Player"))
        {
            hp--;
            VFXManager.Instance.GenerateVFXOnPosition(VFX.HandClap, transform.position, 1);
            AudioManager.Instance.PlaySound(Sounds.HitMetal);
            if (hp <= 0)
            {
                var goal = Instantiate(goalPrefab);
                goal.transform.position = transform.position + Vector3.up * 1;
                Destroy(gameObject);
                foreach (var rb in toBeEnabledUponDeath)
                {
                    rb.isKinematic = false;
                }
            }
            else
            {
                CollectBalls();
            }
        }
    }

    private void ReleaseBalls()
    {
        foreach (var joint in springJointsOnBalls)
        {
            joint.spring = 0;
        }
        foreach (var line in lineRenderings)
        {
            line.DisableLine();
        }
        released = true;
        timer = timerInterval;
    }

    private void CollectBalls()
    {
        foreach (var joint in springJointsOnBalls)
        {
            joint.spring = orginalSpring;
        }
        foreach (var line in lineRenderings)
        {
            line.EnableLine();
        }
        released = false;
        foreach (var mat in runeMats)
        {
            mat.EnableKeyword("_EMISSION");
        }
        disabledRuneCounter = 0;
    }
}
