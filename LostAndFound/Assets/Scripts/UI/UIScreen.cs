using System;
using JetBrains.Annotations;
using UnityEngine;

namespace UI
{
    public class UIScreen : SceneObject
    {
        [SerializeField] private UIController uiController;
        [SerializeField] private Animator animator;
        
        private Action onScreenClosed;
        protected UIController UIController => uiController;
    
        public void Close(Action onComplete = null)
        {
            onScreenClosed = onComplete;
            animator.SetTrigger("Close");
        }

        [UsedImplicitly]
        public void OnScreenClosed()
        {
            Time.timeScale = 1;
            gameObject.SetActive(false);
            onScreenClosed?.Invoke();
        }
    }
}
