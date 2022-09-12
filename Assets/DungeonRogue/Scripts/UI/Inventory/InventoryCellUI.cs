using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace Assets.DungeonRogue.Scripts.UI
{
    public class InventoryCellUI : ItemCellUI
    {
        [SerializeField]
        private InventoryUI inventoryUI = default;

        public override MutualDraggedItemUI GetDraggedItemUI()
        {
            return inventoryUI.DraggedItem;
        }

        protected override void Awake()
        {
            base.Awake();

            inventoryUI = GetComponentInParent<InventoryUI>();
            Assert.IsNotNull(inventoryUI);
        }
    }
}