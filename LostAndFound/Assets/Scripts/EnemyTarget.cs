using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTarget : SceneObject
{
    [SerializeField] Transform enemyTarget = null;




    public Transform getTarget()
    {
        return enemyTarget;
    }


}
