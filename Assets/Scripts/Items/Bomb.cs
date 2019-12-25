using Assets.Scripts.MISC;
using UnityEngine;

public class Bomb : MonoBehaviour, IShootableObject, IDroppableObject
{
    [SerializeField]
    private float power, radius, upForce;
    [SerializeField]
    private Rigidbody rigidbody;

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(transform.position, radius);
    }
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.layer == (int)Layers.Environment)
            Detonate();
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
    }

    public void OnShot()
    {
        OnDrop();
    }
}
