
using System;
using UnityEngine;


public class GameMaster : MonoBehaviour
{
    private static GameMaster instance;

    private void Start()
    {
    }

    public static GameMaster Instance
    {
        get
        {
            if (instance == null){
                GameObject go = new GameObject(nameof(GameMaster));
                instance = go.AddComponent<GameMaster>();
                DontDestroyOnLoad(instance);
            }
            return instance;
        }
    }

    public bool IsGameFinished { get; private set; } = false;
    public bool IsLevelFinished { get; private set; } = false;
    public bool CanTrapsKillEnemies { get; } = true;

    public int CurrentLevel { get; private set; } = -1;

    public void OnLevelFinished()
    {
        IsLevelFinished = true;
        Time.timeScale = 0;
    }

    public void OnGameFinished()
    {
        OnLevelFinished();
        IsGameFinished = true;
    }

    public void StartLevel(int level)
    {
        IsGameFinished = false;
        IsLevelFinished = false;
        CurrentLevel = level;

        Time.timeScale = 1;
    }

    public void ReloadCurrentLevel()
    {
        StartLevel(CurrentLevel);
    }
    
    private GameMaster()
    {
    }
}
