using Assets.DungeonRogue.Scripts.Items;
using UnityEngine;

namespace Assets.DungeonRogue.Scripts.Equipments
{
    public class HandEquipmentSlot : IEquipmentSlot
    {
        public bool EquipItem(IItem item)
        {
            if (item is Weapon)
            {
                Debug.Log("weapon");
            }

            if (item is OrdinaryItem)
            {
                Debug.Log("ordinary item");
            }

            return true;
        }
    }
}