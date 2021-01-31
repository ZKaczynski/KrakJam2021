
using Levels;
using Player;
using UnityEngine;

public class GameMaster : MonoBehaviour
{
    [SerializeField] private LevelLoader levelLoader;
    [SerializeField] private UIController uiController;
    [SerializeField] private Inventory inventory;

    [Header("Game Parameters")]
    [SerializeField] private bool skipMenu = true;
    [SerializeField] private bool canTrapsKillEnemies = true;

    public static GameMaster instance;
    public static GameMaster Instance
    {
        get
        {
            if (instance != null)
            {
                return instance;
            }
            return instance = GameObject.Find(nameof(GameMaster)).GetComponent<GameMaster>();
        }
    }

    public bool IsGameFinished { get; private set; } = false;
    public bool IsLevelFinished { get; private set; } = false;
    public bool CanTrapsKillEnemies => canTrapsKillEnemies;
    public bool SkipMenu => skipMenu;
    public int CurrentLevel { get; private set; } = -1;

    private void Start()
    {
        if (SkipMenu)
        {
            StartLevel(0);
        }
        else
        {
            uiController.OpenMainMenu();
        }
    }

    public void OnVictory()
    {
        IsLevelFinished = true;
        levelLoader.CleanUp();
        if (CurrentLevel == levelLoader.LastLevelIndex)
        {
            IsGameFinished = true;
            uiController.OpenVictoryScreen();
        }
        else
        {
            uiController.OpenFader();
            StartNextLevel();
        }
    }

    public void OnDefeat()
    {
        IsGameFinished = true;
        IsLevelFinished = true;

        levelLoader.CleanUp();
        uiController.OpenDefeatScreen();
    }
    
    public void StartLevel(int level)
    {
        IsGameFinished = false;
        IsLevelFinished = false;
        CurrentLevel = level;
        
        levelLoader.LoadLevel(level);
    }

    public void ReloadCurrentLevel()
    {
        StartLevel(CurrentLevel);
    }
    
    public void StartNextLevel()
    {
        StartLevel(++CurrentLevel);
    }
    
    private GameMaster()
    {
    }

    public Inventory GetInventory()
    {
        return inventory;
    }
}
