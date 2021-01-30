using System;
using LevelMechanics;
using UnityEngine;

namespace Enemy
{
    public class EnemyMovement : SceneObject
    {
        [SerializeField] private float speed = 0.5f;
        [SerializeField] private LayerMask layerMask;


        [SerializeField] public bool InLight;

        [SerializeField] private EnemyTarget target;
        [SerializeField] private Vector2 lastPosition;

        void Start()
        {
            InLight = false;
            lastPosition = transform.position;
            target = null;
        }

        void Update()
        {
            if (GameMaster.IsGameFinished)
            {
                return;
            }
            
            if (target != null && InLight == false)
            {



                float step = speed * Time.deltaTime;
                transform.position = Vector2.MoveTowards(transform.position, lastPosition, step);
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            HasEnteredLight(other);
        }

        private void HasEnteredLight(Collider2D other)
        {
            EnemyTarget potentialTarget = other.gameObject.GetComponent<EnemyTarget>();

            if (potentialTarget != null)
            {
                if (HasLineOfSight(potentialTarget.getTarget(), Color.red))
                {
                    InLight = true;
                    target = potentialTarget;
                    print("LIGHT CENTER!");
                }
            }
        }

        private void OnTriggerStay2D(Collider2D other)
        {
            if (GameMaster.CanTrapsKillEnemies)
            {
                var boobyTrap = other.gameObject.GetComponent<BoobyTrapBehaviour>();
                if(boobyTrap != null && boobyTrap.IsEngaged)
                {
                    Die();
                }
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            EnemyTarget potentialTarget = other.gameObject.GetComponent<EnemyTarget>();

            if (potentialTarget != null)
            {
                if (HasLineOfSight(potentialTarget.getTarget(), Color.green))
                {
                    print("DarkCENTER!!");
                    lastPosition = potentialTarget.getTarget().position;
                    InLight = false;
                }
            }
        }

        private bool HasLineOfSight(Transform other, Color colorOfGizmos)
        {
            Vector3 enemyPosition = transform.position;
            Vector3 targetPosition = other.position;

            float distance = Vector2.Distance(enemyPosition, targetPosition);
            RaycastHit2D hit = Physics2D.Raycast(enemyPosition, targetPosition - enemyPosition, distance, layerMask);

            Debug.DrawRay(enemyPosition, targetPosition - enemyPosition, colorOfGizmos);

            return hit.collider == null;
        }

        private void Die()
        {
            Destroy(gameObject);
        }
    }
}
