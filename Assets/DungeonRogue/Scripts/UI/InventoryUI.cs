using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace Assets.DungeonRogue.Scripts.UI
{
    public class InventoryUI : MonoBehaviour
    {
        [SerializeField]
        private Character character = default;

        [SerializeField]
        private MutualDraggedItemUI draggedItem = default;

        private Inventory characterInventory;

        private List<InventoryCellUI> cellsUI;

        public MutualDraggedItemUI DraggedItem => draggedItem;

        private void Awake()
        {
            Assert.IsNotNull(character);
            Assert.IsNotNull(draggedItem);

            cellsUI = new List<InventoryCellUI>(GetComponentsInChildren<InventoryCellUI>());
        }

        private void Start()
        {
            characterInventory = character.CharacterInventory;

            characterInventory.OnInventoryChanged += OnInventoryChanged;

            foreach (InventoryCellUI cellUI in cellsUI)
            {
                cellUI.OnItemDroppedIntoCell += OnItemDroppedIntoCell;
                cellUI.OnItemToBeDroppedIntoWorld += OnItemToBeDroppedIntoWorld;
            }
        }

        private void OnDestroy()
        {
            characterInventory.OnInventoryChanged -= OnInventoryChanged;

            foreach (InventoryCellUI cellUI in cellsUI)
            {
                cellUI.OnItemDroppedIntoCell -= OnItemDroppedIntoCell;
                cellUI.OnItemToBeDroppedIntoWorld -= OnItemToBeDroppedIntoWorld;
            }
        }

        private void OnInventoryChanged(int index, Cell cellBefore, Cell cellAfter)
        {
            cellsUI[index].SetIcon(cellAfter.StoredItem.Icon);
        }

        private void OnItemDroppedIntoCell(InventoryCellUI sourceCellUI, InventoryCellUI targetCellUI)
        {
            int sourceIndex = cellsUI.IndexOf(sourceCellUI);
            int targetIndex = cellsUI.IndexOf(targetCellUI);

            characterInventory.Swap(sourceIndex, targetIndex);
        }

        private void OnItemToBeDroppedIntoWorld(InventoryCellUI itemCellUI)
        {
            int itemIndex = cellsUI.IndexOf(itemCellUI);

            ItemData itemToBeDroppedData;
            bool isRemovedFromInventory = characterInventory.RemoveOne(itemIndex, out itemToBeDroppedData);

            if (isRemovedFromInventory == true)
            { 
                character.DropItem(itemToBeDroppedData);
            }
        }
    }
}