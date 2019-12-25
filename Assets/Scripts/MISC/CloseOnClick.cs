using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseOnClick : MonoBehaviour
{
    private void Start()
    {
        Time.timeScale = 0;
    }
    public void OnClick()
    {
        gameObject.SetActive(false);
        Time.timeScale = 1;
    }
}
