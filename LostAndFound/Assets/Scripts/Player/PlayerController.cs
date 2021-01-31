using System;
using System.Collections.Generic;
using General;
using LevelMechanics;
using UnityEngine;

namespace Player
{
    public class PlayerController : SceneObject
    {
        [SerializeField] private float speed = 2;
        [SerializeField] private Animator animator;

        private Quaternion lookRotation;
        private Vector3 direction;
        private HashSet<IInteractable> interactablesInRange = new HashSet<IInteractable>();

        private Inventory inventory;
        private static readonly int IsMoving = Animator.StringToHash("IsMoving");

        void Start()
        {
            inventory = GameMaster.GetInventory();
        }

        void Update()
        {
            if (GameMaster.IsGameFinished)
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

            if (Input.GetKeyDown(KeyCode.F))
            {
                TryToUseCurrentItem();
            }
        }

        private void TryToUseCurrentItem()
        {
            if (inventory.HasCurrentItem())
            {
                Throw(inventory.GetCurrentItem());
                Debug.Log("Item used!!");
            }
            else
            {
                Debug.Log("No items");
            }
        }

        private void Move(float inputX, float inputY)
        {
            Vector3 movement = new Vector3(speed * inputX, speed * inputY, 0);

            animator.SetBool(IsMoving, movement.magnitude >= float.Epsilon);
            
            movement *= Time.deltaTime;

            transform.Translate(movement);
        }

        private void InteractWithInteractablesInRange()
        {
            interactablesInRange.Remove(null);

            foreach (var interactable in interactablesInRange)
            {
                interactable.Interact();
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

            var pickUp = other.gameObject.GetComponent<PickUpBehaviour>();
            if (pickUp != null && interactablesInRange.Contains(pickUp) == false)
            {
                interactablesInRange.Add(pickUp);
            }
            
            var tripWire = other.gameObject.GetComponent<TripwireBehaviour>();
            if (tripWire != null && interactablesInRange.Contains(tripWire) == false)
            {
                interactablesInRange.Add(tripWire);
            }
            
            if (other.gameObject.CompareTag("Arrow"))
            {
                Die();
            }

            if (other.gameObject.CompareTag("Finish")){
                GameMaster.OnVictory();
            }
        }

        private void OnTriggerStay2D(Collider2D other)
        {
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

            var pickUp = other.gameObject.GetComponent<PickUpBehaviour>();
            if (pickUp != null)
            {
                interactablesInRange.Remove(pickUp);
            }
            
            var tripWire = other.gameObject.GetComponent<TripwireBehaviour>();
            if (tripWire != null)
            {
                interactablesInRange.Remove(tripWire);
            }
            
            if (other.gameObject.CompareTag("Arrow"))
            {
                Die();
            }
        }
        
        private void Die()
        {
            if (GameMaster.IsGameFinished != true)
            {
                GameMaster.OnDefeat();
            }
        }

        void Throw(GameObject objectToThrow)
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            GameObject projectileInstance = Instantiate(objectToThrow, transform.position, Quaternion.identity);

            projectileInstance.GetComponent<Projectile>().setTarget(mousePos);
            

        }
    }
}

