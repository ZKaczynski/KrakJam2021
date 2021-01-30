using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class UIController : SceneObject
{
    [SerializeField] private GameObject defeatScreen;
    [SerializeField] private GameObject mainMenuScreen;

    public void OpenDefeatScreen()
    {
        defeatScreen.SetActive(true);
        mainMenuScreen.SetActive(false);
    }

    public void OpenMainMenu()
    {
        defeatScreen.SetActive(false);
        mainMenuScreen.SetActive(true);
    }
}
