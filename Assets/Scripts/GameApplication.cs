using UnityEngine;
using System.Collections;

public class GaterElement : MonoBehaviour
{
    public GameApplication app
    {
        get { return GameObject.FindObjectOfType<GameApplication>(); }
    }
}

public class GameApplication : MonoBehaviour
{
    public static string currentLevel = "1";
    public GaterModel model;
    public Map view;
    public GaterController controller;

    private void Start()
    {
    }
}
