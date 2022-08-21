using UnityEngine;

namespace Assets.DungeonRogue.Scripts
{
    public class ItemPicker : MonoBehaviour
    {
        [SerializeField]
        private float maxRaycastDistance = 3.0f;

        public bool AttemptPickUp(Ray ray, out ItemData itemData)
        {
            itemData = new ItemData();

            RaycastHit hitInfo;
            bool raycastResult = Physics.Raycast(ray, out hitInfo, maxRaycastDistance);

            if (raycastResult == false)
            {
                return false;
            }

            bool isPickableItem = ScanHit(hitInfo, out itemData);

            return isPickableItem;
        }

        public bool ScanHit(RaycastHit hitInfo, out ItemData itemData)
        {
            itemData = new ItemData();

            GameObject hitGameObject = hitInfo.collider.gameObject;

            IPickable pickable;
            bool isItem = hitGameObject.TryGetComponent(out pickable);
           
            if (isItem == false)
            {
                return false;
            }

            if (pickable is IItem)
            {
                itemData = (pickable as IItem).GetItemData();

                pickable.PickUp();

                return true;
            }
            else
            { 
                return false;
            }
        }
    }
}