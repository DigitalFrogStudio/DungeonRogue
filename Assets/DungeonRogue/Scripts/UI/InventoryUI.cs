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
        private List<Sprite> iconsMock = default;

        private Inventory characterInventory;

        private List<InventoryCellUI> cellsUI;

        private void Awake()
        {
            Assert.IsNotNull(character);

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
            cellsUI[index].SetIcon(iconsMock[cellAfter.StoredItem.ID - 1]);
        }
    }
}