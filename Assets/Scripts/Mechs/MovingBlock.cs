using Assets.Scripts.MISC;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingBlock : MonoBehaviour
{
    public Axis MovingAxis;
    public float EndPos;
    public float Duration;

    private float originalPos;
    // Start is called before the first frame update
    void Start()
    {
        var mySequence = DOTween.Sequence();
        switch (MovingAxis)
        {
            case Axis.X:
                originalPos = transform.position.x;
                mySequence.Append(transform.DOMoveX(EndPos, Duration));
                mySequence.Append(transform.DOMoveX(originalPos, Duration));
                break;
            case Axis.Y:
                originalPos = transform.position.y;
                mySequence.Append(transform.DOMoveY(EndPos, Duration));
                mySequence.Append(transform.DOMoveY(originalPos, Duration));
                break;
            case Axis.Z:
                originalPos = transform.position.z;
                mySequence.Append(transform.DOMoveZ(EndPos, Duration));
                mySequence.Append(transform.DOMoveZ(originalPos, Duration));
                break;
            default:
                break;
        }
        mySequence.SetLoops(-1, LoopType.Restart);
    }
}
