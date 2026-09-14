using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SwitchLevel_btn : MonoBehaviour
{
    public void onBattonDown(string LevelName)
    {
        LevelManager.Instance.LoadScene(LevelName);
    }

    public void RestartLevel()
    {
        Scene currentScene = SceneManager.GetActiveScene();

        if (currentScene.buildIndex >= 0)
        {
            SceneManager.LoadScene(currentScene.buildIndex);
        }
        else
        {
            SceneManager.LoadScene(currentScene.name);
        }
    }
}
