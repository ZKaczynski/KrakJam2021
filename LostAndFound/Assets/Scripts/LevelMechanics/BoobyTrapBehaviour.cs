using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace LevelMechanics
{
    public class BoobyTrapBehaviour : SceneObject
    {
        [SerializeField] private List<LeverBehaviour> levers; 
        [SerializeField] private Collider2D trapCollider;
        [SerializeField] private SpriteRenderer spriteRenderer;
        
        public bool IsEngaged { get; private set; }

        private void Start()
        {
            if (levers == null || levers.Count == 0)
            {
                Engage(true);
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
            
            Engage(ShouldEngage());
        }

        private void OnLeverStateChangedEvent(LeverBehaviour lever)
        {
            Engage(ShouldEngage());
        }

        private bool ShouldEngage()
        {
            return levers.Any(lever => lever.IsPulled);
        }
        
        private void Engage(bool state)
        {
            IsEngaged = state;
            trapCollider.enabled = IsEngaged;
            spriteRenderer.color = IsEngaged ? Color.red : Color.grey;
        }
    }
}
