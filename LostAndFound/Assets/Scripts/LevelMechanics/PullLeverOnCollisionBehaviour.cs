using Enemy;
using Player;
using UnityEngine;

namespace LevelMechanics
{
    public class PullLeverOnCollisionBehaviour : MonoBehaviour
    {
        [SerializeField] private LeverBehaviour lever;
        [SerializeField] private Collider2D triggerArea;
        [SerializeField] private Collider2D deactivationArea;

        private void OnTriggerEnter2D(Collider2D other)
        {
            bool shouldPull = false;
            var enemy = other.GetComponent<EnemyMovement>();
            if (enemy != null)
            {
                shouldPull = true;
            }
        
            var player = other.gameObject.transform.parent.parent.GetComponent<PlayerController>();
            if (player != null)
            {
                shouldPull = true;
            }

            if (shouldPull)
            {
                lever.Interact();
                triggerArea.enabled = false;
                deactivationArea.enabled = false;
            }
        }
    }
}
