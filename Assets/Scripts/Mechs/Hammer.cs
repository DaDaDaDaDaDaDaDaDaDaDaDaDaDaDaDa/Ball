using Assets.Scripts.MISC;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hammer : MonoBehaviour
{
    [SerializeField]
    private float timeInterval, emoteAddHeight;
    private LineRenderer lineRenderer;
    private SpringJoint joint;
    private float timer;
    private int emoteCounter = 1;
    private const int totalEmoteCount = 4;

    private void Start()
    {
        joint = GetComponent<SpringJoint>();
        lineRenderer = GetComponent<LineRenderer>();
    }

    private void FixedUpdate()
    {
        if (timer < 0)
        {
            var obj = EmotesManager.Instance.GenerateEmoteOnPosition(GetEmotesEnumByCounter(), transform.position + Vector3.up * emoteAddHeight);
            Destroy(obj, 0.5f);
            timer += timeInterval;
            emoteCounter++;
            if (emoteCounter > totalEmoteCount)
            {
                emoteCounter = 1;
                Drop();
            }
            if (emoteCounter == 2)
            {
                Raise();
            }
        }
        timer -= Time.deltaTime;
        lineRenderer.SetPosition(0, transform.position);
        lineRenderer.SetPosition(1, joint.connectedBody.transform.position);
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

    private void Drop()
    {
        joint.spring = 1;
    }

    private void Raise()
    {
        joint.spring = 1000;
    }
}
