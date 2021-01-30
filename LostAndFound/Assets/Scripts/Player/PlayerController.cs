using System.Collections.Generic;
using General;
using LevelMechanics;
using UnityEngine;

namespace Player
{
    public class PlayerController : SceneObject
    {

        [SerializeField] private float speed = 2;

        private Quaternion lookRotation;
        private Vector3 direction;
        private HashSet<IInteractable> interactablesInRange = new HashSet<IInteractable>();

        void Update()
        {
            if (gameMaster.IsGameFinished)
            {
                return;
            }
            
            float inputX = Input.GetAxis("Horizontal");
            float inputY = Input.GetAxis("Vertical");
            Move(inputX, inputY);
            
            if (Input.GetKeyDown(KeyCode.E))
            {
                InteractWithInteractablesInRange();
            }
        }

        private void Move(float inputX, float inputY)
        {
            Vector3 movement = new Vector3(speed * inputX, speed * inputY, 0);

            movement *= Time.deltaTime;

            transform.Translate(movement);
        }

        private void InteractWithInteractablesInRange()
        {
            foreach (var interactable in interactablesInRange)
            {
                if (interactable != null)
                {
                    interactable.Interact();
                }
            }
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.CompareTag("Enemy"))
            {
                Die();
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            var lever = other.gameObject.GetComponent<LeverBehaviour>();
            if(lever != null && interactablesInRange.Contains(lever) == false)
            {
                interactablesInRange.Add(lever);
            }

            var boobyTrap = other.gameObject.GetComponent<BoobyTrapBehaviour>();
            if(boobyTrap != null && boobyTrap.IsEngaged)
            {
                Die();
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            var lever = other.gameObject.GetComponent<LeverBehaviour>();
            if (lever != null)
            {
                interactablesInRange.Remove(lever);
            }
        }

        private void Die()
        {
            gameMaster.OnPlayerDied();
        }
    }
}

