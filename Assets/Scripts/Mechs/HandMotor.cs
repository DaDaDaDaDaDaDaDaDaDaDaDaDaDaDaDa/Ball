using Assets.Scripts.MISC;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandMotor : MonoBehaviour
{
    public float TimeInterval = 1;
    public float Torque;
    private float timer;
    private int emoteCounter = 1;
    private const int totalEmoteCount = 4;
    private Rigidbody rig;
    // Start is called before the first frame update
    void Start()
    {
        rig = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (timer < 0)
        {
            var obj = EmotesManager.Instance.GenerateEmoteOnPosition(GetEmotesEnumByCounter(), transform.position + Vector3.up * 0.5f);
            Destroy(obj, 0.5f);
            timer += TimeInterval;
            emoteCounter++;
            if (emoteCounter > totalEmoteCount)
            {
                emoteCounter = 1;
                Swing();
            }
        }
        timer -= Time.deltaTime;
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

    private void Swing()
    {
        rig.AddTorque(transform.up * Torque, ForceMode.Impulse);
    }
    
}
