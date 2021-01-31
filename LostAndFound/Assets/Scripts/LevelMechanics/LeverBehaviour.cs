using System;
using General;
using JetBrains.Annotations;
using UnityEngine;

namespace LevelMechanics
{
    public class LeverBehaviour : SceneObject, IInteractable
    {
        [SerializeField] [CanBeNull] private SpriteRenderer spriteRenderer;
        [SerializeField] private bool startPulled;
        [SerializeField] private Sprite active;
        [SerializeField] private Sprite off;

        public event Action<LeverBehaviour> LeverStateChangedEvent;

        public bool IsPulled { get; private set; }

        protected override void CustomAwake()
        {
            IsPulled = startPulled;
        }

        private void Start()
        {
            Setup(IsPulled);
        }
        
        public void Interact()
        {
            GetComponent<AudioSource>().Play();

            
            ChangePulledState();
        }

        private void ChangePulledState()
        {
            Setup(!IsPulled);
            OnLeverStateChangedEvent();
        }

        private void Setup(bool state)
        {
            IsPulled = state;
            if (spriteRenderer != null)
            {
                spriteRenderer.sprite = IsPulled ? active : off;
            }
        }

        private void OnLeverStateChangedEvent()
        {
            LeverStateChangedEvent?.Invoke(this);
        }
    }
}
