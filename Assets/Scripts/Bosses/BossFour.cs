using Assets.Scripts.MISC;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFour : MonoBehaviour
{
    [SerializeField]
    private Teleporter telePrefab, topTele;
    [SerializeField]
    private float groundHeight, timeInterval, xMin, xMax, zMin, zMax;
    [SerializeField]
    private int maxTeleCount, hp;
    [SerializeField]
    private GameObject goalPrefab, vfxObj;
    [SerializeField]
    private ContinuousRotating[] rotatingObjs;
    private float timer;

    // Update is called once per frame
    void FixedUpdate()
    {
        timer += Time.deltaTime;
        if (timer > timeInterval)
        {
            GenerateGroundTele();
            timer = 0;
        }
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
                Destroy(vfxObj);
                Destroy(gameObject);
            }
        }
    }

    private void GenerateGroundTele()
    {
        var xPos = Random.Range(xMin, xMax);
        var zPos = Random.Range(zMin, zMax);
        var groundTele = Instantiate(telePrefab);
        groundTele.transform.position = new Vector3(xPos, groundHeight, zPos);
        groundTele.AssignTarget(topTele);
        Destroy(groundTele.gameObject, maxTeleCount * timeInterval);
    }
}
