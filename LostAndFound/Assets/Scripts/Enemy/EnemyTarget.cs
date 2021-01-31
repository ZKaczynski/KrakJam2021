using UnityEngine;

namespace Enemy
{
    public class EnemyTarget : SceneObject
    {
        [SerializeField] Transform enemyTarget = null;




        public Transform getTarget()
        {
            return enemyTarget;
        }


    }
}
