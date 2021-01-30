using System;
using System.Collections.Generic;
using General;
using LevelMechanics;
using UnityEngine;
using UnityEngine.XR.WSA.Input;

namespace Player
{
    public class PlayerController : MonoBehaviour
    {

        [SerializeField] private float speed = 2;

        private Quaternion lookRotation;
        private Vector3 direction;
        private HashSet<IInteractable> interactablesInRange = new HashSet<IInteractable>();

        void Update()
        {
            float inputX = Input.GetAxis("Horizontal");
            float inputY = Input.GetAxis("Vertical");

            Vector3 movement = new Vector3(speed * inputX, speed * inputY, 0);

            movement *= Time.deltaTime;

            transform.Translate(movement);

            if (Input.GetKeyDown(KeyCode.E))
            {
                InteractWithInteractablesInRange();
            }
        }

        void InteractWithInteractablesInRange()
        {
            foreach (var interactable in interactablesInRange)
            {
                if (interactable != null)
                {
                    interactable.Interact();
                }
            }
        }

        void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.CompareTag("Enemy"))
            {
                Debug.Log("death!!");
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            var lever = other.gameObject.GetComponent<LeverBehaviour>();
            if(interactablesInRange.Contains(lever) == false)
            {
                interactablesInRange.Add(lever);
            }

            interactablesInRange.Add(lever);
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            var lever = other.gameObject.GetComponent<LeverBehaviour>();
            if (lever != null)
            {
                interactablesInRange.Remove(lever);
            }
        }
    }
}

