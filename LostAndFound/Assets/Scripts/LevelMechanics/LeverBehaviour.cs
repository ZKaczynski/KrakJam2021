﻿using System;
using General;
using UnityEngine;

namespace LevelMechanics
{
    public class LeverBehaviour : MonoBehaviour, IInteractable
    {
        [SerializeField] private SpriteRenderer spriteRenderer;

        public event Action<LeverBehaviour, bool> LeverStateChangedEvent;
        
        private bool isPulled = false;

        private void Start()
        {
            UpdateGraphics();
        }

        public void Interact()
        {
            isPulled = !isPulled;
            UpdateGraphics();
            OnLeverStateChangedEvent();
        }

        private void UpdateGraphics()
        {
            spriteRenderer.color = isPulled ? Color.green : Color.red;
        }

        private void OnLeverStateChangedEvent()
        {
            LeverStateChangedEvent?.Invoke(this, isPulled);
        }
    }
}