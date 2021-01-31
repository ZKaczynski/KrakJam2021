using System.Collections;
using Enemy;
using Player;
using UnityEngine;

namespace LevelMechanics
{
    public class ArrowBehaviour : MonoBehaviour
    {
        [SerializeField] private float speed;
        [SerializeField] private Transform spriteRendererTransform;

        private Vector3 direction;

        public void Setup(Vector3 direction)
        {
            this.direction = direction.normalized;
            spriteRendererTransform.Rotate(Vector3.forward, Vector3.Angle(Vector3.right, direction));
        }

        private void Update()
        {
            transform.Translate(speed * direction * Time.deltaTime);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            bool targetHit = false;
            var enemy = other.GetComponent<EnemyMovement>();
            if (enemy != null)
            {
                targetHit = true;
            }

            var player = other.gameObject.transform.parent.parent.GetComponent<PlayerController>();
            if (player != null)
            {
                targetHit = true;
            }

            if (other.gameObject.name.Contains("Wall"))
            {
                targetHit = true;
            }

            if (targetHit)
            {
                StartCoroutine(COR_DestroyInNextFrame());
            }
        }
        
        private IEnumerator COR_DestroyInNextFrame()
        {
            yield return new WaitForEndOfFrame();
            Destroy(gameObject);
        }
    }
}
