using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneObject : MonoBehaviour
{
    protected GameMaster GameMaster;
    
    private void Awake()
    {
        GameMaster = GameMaster.Instance;
        CustomAwake();
    }

    protected virtual void  CustomAwake()
    {
    }
}
