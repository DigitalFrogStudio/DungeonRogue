using UnityEngine;
using UnityEngine.Assertions;

namespace Assets.DungeonRogue.Scripts.UI
{
    public class EquipmentCellUI : ItemCellUI
    {
        [SerializeField]
        private EquipmentUI equipmentUI = default;

        public override MutualDraggedItemUI GetDraggedItemUI()
        {
            return equipmentUI.DraggedItem;
        }

        protected override void Awake()
        {
            base.Awake();

            equipmentUI = GetComponentInParent<EquipmentUI>();
            Assert.IsNotNull(equipmentUI);
        }
    }
}