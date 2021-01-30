using System;
using LevelMechanics;
using UnityEngine;

namespace Enemy
{
    public class EnemyMovement : SceneObject
    {
        [SerializeField] private float speed = 0.5f;
        [SerializeField] private LayerMask layerMask;

        public bool InLight { get; private set; }
        
        private Vector2 target;

        void Start()
        {
            target = transform.position;
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

                transform.position = Vector2.MoveTowards(transform.position, target, step);
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            EnterLight(other);
        }

        private void EnterLight(Collider2D other)
        {
            if (other.gameObject.name.Contains("Light"))
            {

                print("Hit by light!");
                float distance = Vector2.Distance(transform.position, other.gameObject.transform.position);

                RaycastHit2D hit = Physics2D.Raycast(other.gameObject.transform.position, transform.position, distance, layerMask);

                Debug.DrawRay(transform.position, other.gameObject.transform.position, Color.red);

                if (hit.collider == null)
                {
                    InLight = true;
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
            EnterLight(other);


        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.gameObject.name.Contains("Light"))
            {
                print("Dark!!");
                float distance = Vector2.Distance(transform.position, other.gameObject.transform.position);

                RaycastHit2D hit = Physics2D.Raycast(other.gameObject.transform.position,transform.position, distance, layerMask);

                Debug.DrawRay(transform.position, other.gameObject.transform.position);

                if (hit.collider == null)
                {
                    print("DarkCENTER!!");
                    target = other.transform.position;
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
