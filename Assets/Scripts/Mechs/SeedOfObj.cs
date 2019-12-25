using Assets.Scripts.MISC;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeedOfObj : MonoBehaviour, IShootableObject
{
    [SerializeField]
    private GameObject prefab;

    public void OnShot()
    {
        return;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == (int)Layers.Environment)
        {
            var obj = Instantiate(prefab);
            obj.transform.position = new Vector3(collision.GetContact(0).point.x, obj.transform.position.y, collision.GetContact(0).point.z);
            VFXManager.Instance.GenerateVFXOnPosition(VFX.Leaves, collision.GetContact(0).point, 2);
            Destroy(gameObject);
        }
    }
}
