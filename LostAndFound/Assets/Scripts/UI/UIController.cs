using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class UIController : SceneObject
{
    [SerializeField] private GameObject defeatScreen;
    [SerializeField] private GameObject mainMenuScreen;
    [SerializeField] private GameObject faderScreen;
    [SerializeField] private GameObject victoryScreen;
    
    public void OpenDefeatScreen()
    {
        defeatScreen.SetActive(true);
        mainMenuScreen.SetActive(false);
        faderScreen.SetActive(false);
        victoryScreen.SetActive(false);
    }

    public void OpenMainMenu()
    {
        defeatScreen.SetActive(false);
        mainMenuScreen.SetActive(true);
        faderScreen.SetActive(false);
        victoryScreen.SetActive(false);
    }

    public void OpenFader()
    {
        defeatScreen.SetActive(false);
        mainMenuScreen.SetActive(false);
        faderScreen.SetActive(true);
        victoryScreen.SetActive(false);
    }
    
    public void OpenVictoryScreen()
    {
        defeatScreen.SetActive(false);
        mainMenuScreen.SetActive(false);
        faderScreen.SetActive(false);
        victoryScreen.SetActive(true);
    }
}
