using LevelMechanics;
using UnityEngine;

namespace Enemy
{
    public class EnemyMovement : SceneObject
    {
        [SerializeField] private float speed = 0.5f;
        [SerializeField] private LayerMask layerMask;
        [SerializeField] public bool InLight;
        [SerializeField] private Transform spriteTransform;

        [SerializeField] private AudioClip[] clips= new AudioClip[10];

        private Transform target;
        private Vector3? lastPosition;
        private bool lastPositionHasMeaningfulValue;

        private AudioSource audio = null;

        private bool ShouldMove => target != null || lastPosition.HasValue;
        private bool CanMove => InLight == false;

        private int lightsCount = 0;

        void Start()
        {
            audio = GetComponent<AudioSource>();
            InLight = false;
            target = null;
        }

        void Update()
        {
            if (GameMaster.IsGameFinished)
            {
                return;
            }

            if (ShouldMove && CanMove)
            {
                if (!audio.isPlaying) {
                    audio.clip = clips[Random.Range(0, clips.Length)];
                    audio.Play();
                }
                if (target != null && HasLineOfSight(target, Color.clear))
                {
                    lastPosition = target.position;

                    Vector3 objectPos = transform.position;

                    Vector3 dir = (Vector3)(lastPosition - transform.position);

                    //targ.x = targ.x - objectPos.x;
                    //targ.y = targ.y - objectPos.y;

                    float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
                    transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

                    /*
                    Vector3 vectorToTarget = (Vector3)(lastPosition - transform.position);
                    float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
                    Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
                    transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * speed);
                    */

                    //transform.LookAt(target.position);


                    //Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

                    //Vector2 perpendicular = Vector3.Cross((Vector3)(transform.position - lastPosition), Vector3.forward);
                    //transform.rotation = Quaternion.LookRotation(Vector3.forward, perpendicular);

                    /*
                    float dotProduct = Vector3.Dot(Vector2.up, (transform.position - target.position).normalized);
                    float multiplier = dotProduct < 0 ? 1 : -1;
                    spriteTransform.rotation = Quaternion.Euler(0, 0, multiplier * Vector2.Angle(Vector2.up, (transform.position - target.position).normalized));*/
                }

                if (lastPosition.HasValue)
                {
                    float step = speed * Time.deltaTime;
                    transform.position = Vector2.MoveTowards(transform.position, lastPosition.Value, step);

                    if (Vector2.Distance(lastPosition.Value, transform.position) <= float.Epsilon)
                    {
                        lastPosition = null;
                        target = null;
                    }
                }
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
                    lightsCount++;
                    InLight = true;
                    target = potentialTarget.getTarget();
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
                    target = potentialTarget.getTarget();
                    lightsCount--;
                    if (lightsCount == 0)
                    {
                        InLight = false;
                    }
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
