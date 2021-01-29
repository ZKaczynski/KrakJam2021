using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class SceneObject : MonoBehaviour
{
    private GameMaster _gameMaster;
    
    private void Awake()
    {
        CustomAwake();
    }

    private void CustomAwake()
    {
        _gameMaster = GameMaster.GetInstance();
    }
}
