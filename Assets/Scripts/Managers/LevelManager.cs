using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>跨场景保留的关卡切换入口，供菜单和按钮调用。</summary>
public class LevelManager : MonoBehaviour
{
    private static LevelManager instance;

    public static LevelManager Instance { get => instance; set => instance = value; }

    private void Awake()
    {
        // 切换关卡后按钮仍能访问同一个管理器。
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

   /// <summary>按 Build Settings 中注册的场景名加载关卡。</summary>
   public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
