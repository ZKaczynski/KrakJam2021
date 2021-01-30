using System;
using General;
using UnityEngine;

namespace LevelMechanics
{
    public class LeverBehaviour : MonoBehaviour, IInteractable
    {
        [SerializeField] private SpriteRenderer spriteRenderer;

        public event Action<LeverBehaviour, bool> LeverStateChangedEvent;
        
        private bool isPulled = false;
        
        public void Interact()
        {
            isPulled = !isPulled;
            UpdateGraphics();
            OnLeverStateChangedEvent();
        }

        public void UpdateGraphics()
        {
            spriteRenderer.color = isPulled ? Color.white : Color.green;
        }

        private void OnLeverStateChangedEvent()
        {
            LeverStateChangedEvent?.Invoke(this, isPulled);
        }
    }
}
