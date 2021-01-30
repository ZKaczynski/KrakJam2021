using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneObject : MonoBehaviour
{
    protected GameMaster gameMaster;
    
    private void Awake()
    {
        CustomAwake();
    }

    private void CustomAwake()
    {
        gameMaster = GameMaster.Instance;
    }
}
