using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnMainMenu_btn : MonoBehaviour
{
    public void onBattonDown(string LevelName)
    {
        LevelManager.Instance.LoadScene(LevelName);
    }
}
