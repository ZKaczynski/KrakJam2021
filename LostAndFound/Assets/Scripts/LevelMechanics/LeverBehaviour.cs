using System;
using General;
using UnityEngine;

namespace LevelMechanics
{
    public class LeverBehaviour : SceneObject, IInteractable
    {
        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private bool startPulled;

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
            spriteRenderer.color = IsPulled ? Color.green : Color.yellow;
        }

        private void OnLeverStateChangedEvent()
        {
            LeverStateChangedEvent?.Invoke(this);
        }
    }
}
