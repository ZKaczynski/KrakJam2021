using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster
{
    private static readonly GameMaster Instance = null;

    public static GameMaster GetInstance()
    {
        return Instance ?? new GameMaster();
    }
    
    public TransformUtils TransformUtils = new TransformUtils();
    
    private GameMaster()
    {
    }
}
