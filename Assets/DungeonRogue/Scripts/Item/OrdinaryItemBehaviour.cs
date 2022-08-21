using UnityEditor;
using UnityEngine;

namespace Assets.DungeonRogue.Scripts
{
    public class OrdinaryItemBehaviour : ItemBehaviour<OrdinaryItem>, IPickable
    {
        public void PickUp()
        {
            Destroy(gameObject);
        }

        protected override void Awake()
        {
            base.Awake();
        }

        protected override OrdinaryItem CreateItemData(int id)
        {
            return new OrdinaryItem(id);
        }
    }
}