using System.Collections;
using General;
using UnityEngine;

namespace LevelMechanics
{
    public class PickUpBehaviour : SceneObject, IInteractable
    {
        public ItemType pickUpType = ItemType.Flare;
        
        public void Interact()
        {
            PickUp();
        }

        private void PickUp()
        {
            GameMaster.GetInventory().AddItem(pickUpType);
            StartCoroutine(COR_DestroyInNextFrame());
        }

        private IEnumerator COR_DestroyInNextFrame()
        {
            yield return new WaitForEndOfFrame();
            Destroy(gameObject);
        }
    }
}
