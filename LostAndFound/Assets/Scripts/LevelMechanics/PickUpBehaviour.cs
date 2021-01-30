using General;

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
            Destroy(gameObject);
        }
    }
}
