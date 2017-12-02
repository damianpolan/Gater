using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelectPage : MonoBehaviour
{
    string levelButtonPrefabPath = "Prefabs/LevelButton";
    public int pageNumber = 0;
    public int pageSize = 6;
    string[] mapPaths;

    public void NextPage()
    {
        if (!PageIsHighest(pageNumber))
        {
            pageNumber++;
            UpdateNextPreviousButtons();
            ReloadPage();
        }
    }

    public void PreviousPage()
    {
        if (!PageIsLowest(pageNumber))
        {
            pageNumber--;
            UpdateNextPreviousButtons();
            ReloadPage();
        }
    }

    public void LoadLevel(string path)
    {
        SceneManager.LoadScene(1);
    }

    bool PageIsLowest(int targetPageNumber)
    {
        return targetPageNumber <= 0;
    }

    bool PageIsHighest(int targetPageNumber)
    {
        int pageOfHighestMapIndex = (int) Math.Floor((mapPaths.Length - 1) / (float) pageSize);
        return pageOfHighestMapIndex == targetPageNumber;
    }

    void UpdateNextPreviousButtons()
    {
        Transform previousButton = gameObject.transform
            .Find("BackSeperator")
            .Find("TitlePanel")
            .Find("PreviousButton");
        Transform nextButton = gameObject.transform
            .Find("BackSeperator")
            .Find("TitlePanel")
            .Find("NextButton");

        previousButton.gameObject.GetComponent<Button>().interactable = true;
        nextButton.gameObject.GetComponent<Button>().interactable = true;

        if (PageIsLowest(pageNumber))
            previousButton.GetComponent<Button>().interactable = false;
        
        if (PageIsHighest(pageNumber))
            nextButton.gameObject.GetComponent<Button>().interactable = false;
    }

    void Start () {
        mapPaths = MapPaths("Assets/Resources/Maps/");
        ReloadPage();
    }

    
    void ReloadPage()
    {
        Transform levelsPanel = gameObject.transform.Find("LevelsPanel");

        foreach (Transform child in levelsPanel)
        {
            GameObject.Destroy(child.gameObject);
        }

        int start_index = pageNumber * pageSize;
        int end_index = Mathf.Min(start_index + pageSize, mapPaths.Length);
        for (int i = start_index; i < end_index; i++)
        {
            GameObject obj = GameObject.Instantiate(Resources.Load(levelButtonPrefabPath)) as GameObject;
            Text text = obj.GetComponentInChildren<Text>();
            LoadLevelOnClick levelLoader = obj.GetComponentInChildren<LoadLevelOnClick>();

            text.text = mapName(mapPaths[i]);
            levelLoader.level = mapPaths[i];

            obj.transform.SetParent(levelsPanel);
        }

        UpdateNextPreviousButtons();
    }

    void Update () {


    }

    string mapName(string path)
    {
        return Path.GetFileNameWithoutExtension(path);
    }

    string[] MapPaths(string path)
    {
        List<string> paths = new List<string>();

        foreach (string levelPath in System.IO.Directory.GetFiles(path))
        {
            if (Path.GetExtension(levelPath) == ".txt")
            {
                string withoutAssetsFolder = levelPath
                    .Substring(levelPath.IndexOf("Maps"));
                withoutAssetsFolder = withoutAssetsFolder
                    .Substring(0, withoutAssetsFolder.IndexOf("."));
                paths.Add(withoutAssetsFolder);
            }
            
        }
        
        return paths.ToArray();
    }
}
