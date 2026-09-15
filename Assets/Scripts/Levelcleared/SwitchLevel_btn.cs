using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>关卡界面按钮入口：切换指定场景或重开当前场景。</summary>
public class SwitchLevel_btn : MonoBehaviour
{
    /// <summary>由 Button 传入目标场景名。</summary>
    public void onBattonDown(string LevelName)
    {
        LevelManager.Instance.LoadScene(LevelName);
    }

    /// <summary>无需手填关卡名，直接重载当前活动场景。</summary>
    public void RestartLevel()
    {
        Scene currentScene = SceneManager.GetActiveScene();

        if (currentScene.buildIndex >= 0)
        {
            // 已注册场景用序号重载，避免场景重名。
            SceneManager.LoadScene(currentScene.buildIndex);
        }
        else
        {
            // 编辑器中未注册的场景回退到名称。
            SceneManager.LoadScene(currentScene.name);
        }
    }
}
