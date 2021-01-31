using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace LevelMechanics
{
    public class DispenserBehaviour : SceneObject
    {
        [SerializeField] private Transform origin;
        [SerializeField] private Transform target;
        [SerializeField] private ArrowBehaviour arrowPrefab;
        [SerializeField] private List<LeverBehaviour> levers;
        
        private bool ShouldShoot => levers.All(lever => lever.IsPulled);
        
        private void Start()
        {
            if (levers == null || levers.Count == 0)
            {
                Shoot();
                return;
            }

            foreach (var lever in levers)
            {
                if (lever == null)
                {
                    continue;
                }
                
                lever.LeverStateChangedEvent -= OnLeverStateChangedEvent;
                lever.LeverStateChangedEvent += OnLeverStateChangedEvent;
            }

            if (ShouldShoot)
            {
                Shoot();
            }
        }
        private void  OnLeverStateChangedEvent(LeverBehaviour lever)
        {
            if (lever == null)
            {
                return;
            }
            
            if (ShouldShoot)
            {
                Shoot();
            }
        }

        private void Shoot()
        {
            ArrowBehaviour arrow = Instantiate(arrowPrefab, origin);
            arrow.Setup(target.position - origin.position);
        }
    }
}
