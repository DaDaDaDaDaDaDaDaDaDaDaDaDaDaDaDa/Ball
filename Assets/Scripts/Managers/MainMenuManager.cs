using Assets.Scripts.MISC;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : Singleton<MainMenuManager>
{
    public void JumpToLevel(int index)
    {
        AudioManager.Instance.PlaySound(Sounds.Yeeha);
        StartCoroutine(LoadSceneInSeconds(1.2f, index));
    }

    IEnumerator LoadSceneInSeconds(float sec, int index)
    {
        yield return new WaitForSeconds(sec);
        SceneManager.LoadScene(index);
    }
}
