using Assets.Scripts.MISC;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneManager : Singleton<GameSceneManager>
{
    private bool paused;

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void OnWin()
    {
        PauseGame();
        NotificationManager.Instance.ShowWinNotification(SpawnManager.Instance.GetStepCount());
        AudioManager.Instance.PlaySound(Sounds.Cheers);
        LoadNextLevel();
    }

    public void OnLose()
    {
        PauseGame();
        NotificationManager.Instance.ShowLoseNotification();
        AudioManager.Instance.PlaySound(Sounds.Bhoo);
    }
    
    public void LoadNextLevel()
    {
        if (SceneManager.sceneCountInBuildSettings > SceneManager.GetActiveScene().buildIndex + 1)
            StartCoroutine(StartNewSceneInSeconds(2, SceneManager.GetActiveScene().buildIndex + 1));
    }

    public bool IsGamePaused()
    {
        return paused;
    }

    public void PauseGame()
    {
        paused = true;
    }

    public void ResumeGame()
    {
        paused = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            Restart();

        //Back To Main Menu
        if (Input.GetKeyDown(KeyCode.Escape))
            SceneManager.LoadScene(SceneManager.sceneCountInBuildSettings - 1);
    }

    IEnumerator StartNewSceneInSeconds(float sec, int sceneIndex)
    {
        yield return new WaitForSeconds(sec);
        SceneManager.LoadScene(sceneIndex);
    }
}
