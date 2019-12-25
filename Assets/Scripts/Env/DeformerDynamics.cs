using UnityEngine;
using DG.Tweening;

public class DeformerDynamics : MonoBehaviour
{
    [SerializeField]
    private Transform deformerTransform;
    [SerializeField]
    private float strength, duration;

    private void OnCollisionEnter(Collision collision)
    {
            deformerTransform.DOShakePosition(duration, strength);
    }
}
