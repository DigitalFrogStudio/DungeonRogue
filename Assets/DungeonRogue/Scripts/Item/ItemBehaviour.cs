using UnityEngine;
using UnityEngine.Assertions;

namespace Assets.DungeonRogue.Scripts
{
    public abstract class ItemBehaviour<T> : MonoBehaviour, IItem where T : IItem
    {
        [SerializeField]
        private int id = 0;

        private T itemData;

        public ItemData GetItemData()
        {
            return itemData.GetItemData();
        }

        protected virtual void Awake()
        {
            Assert.IsTrue(id > 0);

            itemData = CreateItemData(id);
        }

        protected abstract T CreateItemData(int id);
    }
}