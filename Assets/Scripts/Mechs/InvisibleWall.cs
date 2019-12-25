using Assets.Scripts.MISC;
using UnityEngine;

public class InvisibleWall : MonoBehaviour
{
    [SerializeField]
    private float returnForce;
    void OnCollisionEnter(Collision other)
    {
        var hitPoint = other.contacts[0].point;
        other.gameObject.GetComponent<Rigidbody>().AddForce((other.transform.position - hitPoint) * returnForce, ForceMode.Impulse);
        AudioManager.Instance.PlaySound(Sounds.Electric);
        VFXManager.Instance.GenerateVFXOnPosition(VFX.WallReaction, hitPoint, transform.localRotation.eulerAngles, 1);
    }
}
