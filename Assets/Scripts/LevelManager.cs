using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    private static LevelManager instance;

    public static LevelManager Instance { get => instance; set => instance = value; }

    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

   public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
