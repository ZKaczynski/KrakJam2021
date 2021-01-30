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
            if (gameMaster.IsGameFinished)
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

                float distance = Vector2.Distance(transform.position, potentialTarget.getTarget().position);
                

                RaycastHit2D hit = Physics2D.Raycast(potentialTarget.getTarget().position, transform.position, distance, layerMask);

                Debug.DrawRay(transform.position, potentialTarget.getTarget().position, Color.red);

                if (hit.collider == null)
                {
                    InLight = true;
                    target = potentialTarget;
                    print("LIGHT CENTER!");
                }
            }
        }

        private void OnTriggerStay2D(Collider2D other)
        {
            if (gameMaster.CanTrapsKillEnemies)
            {
                var boobyTrap = other.gameObject.GetComponent<BoobyTrapBehaviour>();
                if(boobyTrap != null && boobyTrap.IsEngaged)
                {
                    Die();
                }
            }
            HasEnteredLight(other);


        }

        private void OnTriggerExit2D(Collider2D other)
        {
            EnemyTarget potentialTarget = other.gameObject.GetComponent<EnemyTarget>();

            if (potentialTarget != null)
            {
                print("Dark!!");
                float distance = Vector2.Distance(transform.position, potentialTarget.getTarget().position);

                RaycastHit2D hit = Physics2D.Raycast(potentialTarget.getTarget().position, transform.position, distance, layerMask);

                Debug.DrawRay(transform.position, other.gameObject.transform.position);

                if (hit.collider == null)
                {
                    print("DarkCENTER!!");
                    lastPosition = potentialTarget.getTarget().position;
                    InLight = false;
                }

            }
        }

        private void Die()
        {
            Destroy(gameObject);
        }
    }
}
