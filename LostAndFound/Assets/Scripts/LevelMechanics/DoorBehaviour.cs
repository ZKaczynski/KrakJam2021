using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

namespace LevelMechanics
{
    public class DoorBehaviour : MonoBehaviour
    {
        [SerializeField] private List<LeverBehaviour> levers;
        [SerializeField] private SpriteRenderer spriteRendererToDisable;
        [SerializeField] private Rigidbody2D rigidbodyToDisable;
        [SerializeField] private List<ShadowCaster2D> shadowCastersToDisable;

        private Dictionary<LeverBehaviour, bool> leversToStates = new Dictionary<LeverBehaviour, bool>();
        
        private void Start()
        {
            if (levers == null || levers.Count == 0)
            {
                Open();
                return;
            }
            
            foreach (var lever in levers)
            {
                if (lever == null)
                {
                    continue;
                }
                
                leversToStates[lever] = false;
                
                lever.LeverStateChangedEvent -= OnLeverStateChangedEvent;
                lever.LeverStateChangedEvent += OnLeverStateChangedEvent;
            }
        }

        private void  OnLeverStateChangedEvent(LeverBehaviour lever, bool isPulled)
        {
            if (lever == null)
            {
                return;
            }
            
            leversToStates[lever] = isPulled;
            if (ShouldOpen())
            {
                Open();
            }
        }

        private bool ShouldOpen()
        {
            return leversToStates.Values.All(state => state);
        }

        private void Open()
        {
            spriteRendererToDisable.enabled = false;
            shadowCastersToDisable.ForEach(sc => sc.castsShadows = false);
            rigidbodyToDisable.simulated = false;
        }
    }
}
