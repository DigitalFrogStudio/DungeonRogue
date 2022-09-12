using Assets.DungeonRogue.Scripts.Equipments;

namespace Assets.DungeonRogue.Scripts
{
    public class Equipment
    {
        HandEquipmentSlot leftHandSlot;

        HandEquipmentSlot rightHandSlot;

        public Equipment()
        {
            leftHandSlot = new HandEquipmentSlot();

            rightHandSlot = new HandEquipmentSlot();
        }
    }
}