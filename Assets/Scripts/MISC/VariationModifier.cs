using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VariationModifier : MonoBehaviour
{
    [SerializeField]
    private float rangeFactor;
    private void Start()
    {
        foreach (Transform child in transform)
        {
            var scale = child.localScale;
            child.localScale = new Vector3(scale.x * Random.Range(1 - rangeFactor, 1 + rangeFactor), scale.y * Random.Range(1 - rangeFactor, 1 + rangeFactor), scale.z * Random.Range(1 - rangeFactor, 1 + rangeFactor));
            child.localEulerAngles = new Vector3(0, Random.Range(0, 360), 0);
        }
    }
}
