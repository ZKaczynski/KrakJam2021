using System.Collections;
using General;
using UnityEngine;
using UnityEngine.Serialization;

namespace LevelMechanics
{
    public class TripwireBehaviour : SceneObject, IInteractable
    {
        [SerializeField] private Collider2D deactivationArea;
        [SerializeField] private Collider2D triggerArea;
        [SerializeField] private SpriteRenderer spriteRenderer;

        public bool IsActive { get; private set; } = true;

        public void Interact()
        {
            Deactivate();
        }

        private void Deactivate()
        {
            IsActive = false;
            StartCoroutine(COR_DestroyInNextFrame());
        }
        
        private IEnumerator COR_DestroyInNextFrame()
        {
            yield return new WaitForEndOfFrame();
            deactivationArea.enabled = false;
            triggerArea.enabled = false;
            spriteRenderer.enabled = false;
        }
    }
}