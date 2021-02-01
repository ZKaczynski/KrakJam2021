using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using LevelMechanics;
using UnityEngine;

namespace Enemy
{
    public class EnemyMovement : SceneObject
    {
        [SerializeField] private float speed = 0.5f;
        [SerializeField] private LayerMask layerMask;
        [SerializeField] public bool InLight;
        [SerializeField] [ItemCanBeNull] private List<Transform> visibilityCheckMarkers;
        [SerializeField] private Rigidbody2D rigidbody2D;

        [SerializeField] private AudioClip[] clips= new AudioClip[10];

        private Transform target;
        private Vector3? lastPosition;
        private AudioSource audio = null;

        private bool ShouldMove => target != null || lastPosition.HasValue;
        private bool CanMove => InLight == false;

        private List<EnemyTarget> lightSourcesInRange = new List<EnemyTarget>();

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

            UpdateInLightStatus();
            
            if (ShouldMove && CanMove)
            {
                if (!audio.isPlaying) {
                    audio.clip = clips[Random.Range(0, clips.Length)];
                    audio.Play();
                }
                if (target != null && HasLineOfSight(target, Color.clear))
                {
                    lastPosition = target.position;
                    
                    Vector3 dir = (Vector3)(lastPosition - transform.position);
                    float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
                    transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
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

        private void UpdateInLightStatus()
        {
            lightSourcesInRange.RemoveAll(x => x == null);
            
            EnemyTarget potentialTarget = lightSourcesInRange.FirstOrDefault(x => IsVisibleFrom(visibilityCheckMarkers, x.getTarget(), Color.clear));
            if (potentialTarget != null)
            {
                InLight = true;
                target = potentialTarget.getTarget();
            }
            else
            {
                InLight = false;
            }
            
            rigidbody2D.constraints = InLight ? RigidbodyConstraints2D.FreezeAll : RigidbodyConstraints2D.None;
        }
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            EnemyTarget potentialTarget = other.gameObject.GetComponent<EnemyTarget>();

            if (potentialTarget != null)
            {
                lightSourcesInRange.Add(potentialTarget);
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
                lightSourcesInRange.Remove(potentialTarget);
            }
        }

        private bool IsVisibleFrom(List<Transform> markers, Transform other, Color colorOfGizmos)
        {
            bool isAnyMarkerVisible = false;
            foreach (var marker in markers)
            {
                if (marker == null)
                {
                    continue;
                }
                
                Vector3 markerPosition = marker.position;
                Vector3 targetPosition = other.position;

                float distance = Vector2.Distance(markerPosition, targetPosition);
                
                RaycastHit2D hit = Physics2D.Raycast(markerPosition, targetPosition - markerPosition, distance, layerMask);
                Debug.DrawRay(markerPosition, targetPosition - markerPosition, colorOfGizmos);
                bool markerVisible = hit.collider == null;
                isAnyMarkerVisible |= markerVisible;

                if (markerVisible)
                {
                    break;
                }
            }

            return isAnyMarkerVisible;
        }
        
        private bool HasLineOfSight(Transform other, Color colorOfGizmos)
        {
            return IsVisibleFrom(new List<Transform>{ transform }, other, colorOfGizmos);
        }

        private void Die()
        {
            Destroy(gameObject);
        }
    }
}
