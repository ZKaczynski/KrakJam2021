using UnityEngine;

namespace LevelMechanics
{
    public class Projectile : MonoBehaviour
    {
        private Vector3 target;

        public float speed = 10f;

        void Update()
        {
            if (target != null)
            {

                Vector2 movement = target - transform.position;

                movement *= Time.deltaTime* speed;

                transform.Translate(movement);

            }
        }

        public void setTarget(Vector3 target)
        {
            this.target = target;
        }
    }
}
