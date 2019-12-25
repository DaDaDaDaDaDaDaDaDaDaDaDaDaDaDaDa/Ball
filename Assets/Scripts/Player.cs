using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private string goalTag, enemyTag;
    void OnCollisionEnter(Collision other)
    {
        if (GameSceneManager.Instance.IsGamePaused())
            return;
        if (other.gameObject.tag == goalTag)
            GameSceneManager.Instance.OnWin();
        if (other.gameObject.tag == enemyTag)
            GameSceneManager.Instance.OnLose();
    }
}
