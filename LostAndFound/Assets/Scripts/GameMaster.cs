
using UnityEngine;


public class GameMaster : MonoBehaviour
{
    private static GameMaster instance;
    
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

    public void OnLevelFinished()
    {
        IsLevelFinished = true;
    }

    public void OnPlayerDied()
    {
        IsGameFinished = true;
    }
    
    private GameMaster()
    {
    }
}
