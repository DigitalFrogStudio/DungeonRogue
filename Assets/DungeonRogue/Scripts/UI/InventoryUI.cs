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
        }

        private void OnEnable()
        {
            if (characterInventory != null)
            { 
                characterInventory.OnInventoryChanged += OnInventoryChanged;
            }
        }

        private void OnDisable()
        {
            characterInventory.OnInventoryChanged -= OnInventoryChanged;
        }

        private void OnInventoryChanged(int index, Cell cellBefore, Cell cellAfter)
        {
            cellsUI[index].SetIcon(cellAfter.StoredItem.Icon);
        }
    }
}