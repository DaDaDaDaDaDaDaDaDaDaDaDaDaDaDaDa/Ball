using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NotificationManager : Singleton<NotificationManager>
{
    [SerializeField]
    private GameObject canvasObj;
    [SerializeField]
    private Text notificationText;

    public void ShowWinNotification(int stepCount)
    {
        canvasObj.SetActive(true);
        notificationText.text = $"步数：{stepCount}\n2秒后加载下一关";
    }
    public void ShowLoseNotification()
    {
        canvasObj.SetActive(true);
        notificationText.text = "朋友，是另外一个球！\n(按空格重新开始)";
    }
}
