using System.Collections.Generic;
using LevelMechanics;
using UnityEngine;

namespace Player
{
    public class Inventory : MonoBehaviour
    {

        private ItemType currentItem = 0;
        private Dictionary<ItemType, int> items = new Dictionary<ItemType, int>();

        [SerializeField] private  GameObject[] itemsPrefabs = new GameObject[1];

        internal void AddItem(ItemType pickUpType)
        {
            if(items.TryGetValue(pickUpType, out int amount))
            {
                items[pickUpType] = amount + 1;
            }
            else
            {
                items[pickUpType] = 1;
            }
        }

        public GameObject GetCurrentItem()
        {
            if (currentItem == ItemType.Flare)
            {
                return itemsPrefabs[0];
            }
            return null;
        }

        internal bool HasCurrentItem()
        {
            if(items.TryGetValue(currentItem, out int amount))
            {
                if (amount > 0)
                {
                    items[currentItem] = amount - 1;
                    return true;
                }
            }
            return false;
        }
    }
}
