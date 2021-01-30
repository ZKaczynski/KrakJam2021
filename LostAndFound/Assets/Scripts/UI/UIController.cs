using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class UIController : SceneObject
{
    [SerializeField] private GameObject defeatScreen;
    [SerializeField] private GameObject mainMenuScreen;

    private void Start()
    {
        OpenMainMenu();
    }

    private void Update()
    {
        if (GameMaster.IsGameFinished)
        {
            OpenDefeatScreen();
        }
    }

    private void OpenDefeatScreen()
    {
        Time.timeScale = 0;
        defeatScreen.SetActive(true);
        mainMenuScreen.SetActive(false);
    }

    private void OpenMainMenu()
    {
        Time.timeScale = 0;
        defeatScreen.SetActive(false);
        mainMenuScreen.SetActive(true);
    }
}
