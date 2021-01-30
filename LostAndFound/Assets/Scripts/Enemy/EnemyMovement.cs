﻿using UnityEngine;

namespace Enemy
{
    public class EnemyMovement : SceneObject
    {
        [SerializeField] private float speed = 0.5f;

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
            if (other.gameObject.name.Contains("Light"))
            { 
                //  RaycastHit2D hit = Physics2D.Raycast(transform.position, other.gameObject.transform.position);

                //  if (hit.collider == null)
                // //  {
                InLight = true; 
                //  }
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.gameObject.name.Contains("Light"))
            {
                // RaycastHit2D hit = Physics2D.Raycast(transform.position, other.gameObject.transform.position);
                // Debug.Log(hit)

                //if (hit.collider == null)
                // {

                target = other.transform.position;
                InLight = false;

                // }
            }
        }
    }
}
