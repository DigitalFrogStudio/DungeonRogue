using Assets.DungeonRogue.Scripts.Items;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.DungeonRogue.Scripts
{
    public class ItemDropper : MonoBehaviour
    {
        [SerializeField]
        private Transform dropOrigin = default;

        [SerializeField]
        private float maxDropDistance  = 1f;

        public void Drop(ItemData itemData)
        {
            RaycastHit raycastHit;
            bool hitResult = Physics.Raycast(dropOrigin.position, dropOrigin.forward, out raycastHit, maxDropDistance);

            float dropDistance;
            if (hitResult == false)
            {
                dropDistance = maxDropDistance;
            }
            else
            {
                dropDistance = raycastHit.distance;
            }

            Vector3 dropOffset = new Vector3(0f, 0f, dropDistance);
            Instantiate(
                ItemsDataStorage.IDToItemPrefabDictionary[itemData.ID],
                dropOrigin.TransformPoint(dropOffset),
                transform.rotation);
        }
    }
}