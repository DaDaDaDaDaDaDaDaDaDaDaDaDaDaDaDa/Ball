using Assets.Scripts.MISC;
using DG.Tweening;
using UnityEngine;

public class ContinuousRotating : MonoBehaviour
{
    [SerializeField]
    private Axis rotatingAxis;
    [SerializeField]
    private float speed;
    private Vector3 rotationDirection;
    private bool disabled;
    private void Start()
    {
        switch (rotatingAxis)
        {
            case Axis.X:
                rotationDirection = new Vector3(1, 0, 0);
                break;
            case Axis.Y:
                rotationDirection = new Vector3(0, 1, 0);
                break;
            case Axis.Z:
                rotationDirection = new Vector3(0, 0, 1);
                break;
        }
    }
    private void FixedUpdate()
    {
        if (disabled)
            return;
        transform.Rotate(rotationDirection * speed, Space.Self);
    }

    public void DisableRotation()
    {
        disabled = true;
    }
    public void EnableRotation()
    {
        disabled = false;
    }
}
