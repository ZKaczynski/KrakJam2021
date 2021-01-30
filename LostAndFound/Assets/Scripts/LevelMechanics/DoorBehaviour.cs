using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

namespace LevelMechanics
{
    public class DoorBehaviour : SceneObject
    {
        [SerializeField] private List<LeverBehaviour> levers;
        [SerializeField] private SpriteRenderer spriteRendererToDisable;
        [SerializeField] private Rigidbody2D rigidbodyToDisable;
        [SerializeField] private List<ShadowCaster2D> shadowCastersToDisable;
        [SerializeField] private bool allowClose;
        
        public bool IsOpened { get; private set; }
        
        private void Start()
        {
            if (levers == null || levers.Count == 0)
            {
                Open(true);
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

            Open(ShouldOpen());
        }

        private void  OnLeverStateChangedEvent(LeverBehaviour lever)
        {
            if (lever == null)
            {
                return;
            }
            
            if (ShouldOpen())
            {
                Open(true);
            }
            else if (allowClose)
            {
                Open(false);
            }
        }

        private bool ShouldOpen()
        {
            return levers.All(lever => lever.IsPulled);
        }
        
        private void Open(bool state)
        {
            IsOpened = state == false;
            spriteRendererToDisable.enabled = IsOpened;
            shadowCastersToDisable.ForEach(sc => sc.castsShadows = IsOpened);
            rigidbodyToDisable.simulated = IsOpened;
        }
    }
}
