using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadLevelOnClick : MonoBehaviour {
    public int mapSceneIndex = 1;
    public string level = "1";

    public void LoadMap()
    {
        GameApplication.currentLevel = level;
        SceneManager.LoadScene(mapSceneIndex);
    }
}
