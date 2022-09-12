using Assets.DungeonRogue.Scripts.Items;

namespace Assets.DungeonRogue.Scripts.Equipments
{
    public interface IEquipmentSlot
    {
        public bool EquipItem(IItem item);
    }
}