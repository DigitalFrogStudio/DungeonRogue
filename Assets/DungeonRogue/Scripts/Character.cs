using UnityEngine;
using UnityEngine.Assertions;

namespace Assets.DungeonRogue.Scripts
{
    public class Character : MonoBehaviour
    {
        [SerializeField]
        private ItemPicker itemPicker = default;

        [SerializeField]
        private int inventoryCellsAmount = 5;

        public Inventory CharacterInventory { get; private set; }

        public bool AttemptPickUp(Ray ray)
        {
            ItemData itemData;
            
            bool isPickedUp = itemPicker.AttemptPickUp(ray, out itemData);

            if (isPickedUp == false)
            {
                return false;
            }

            CharacterInventory.AttemptAdd(itemData);

            return true;
        }

        private void Awake()
        {
            Assert.IsNotNull(itemPicker);

            CharacterInventory = new Inventory(inventoryCellsAmount);
        }

        private void Start()
        {
            CharacterInventory.OnInventoryChanged += OnInventoryChanged;
        }

        private void OnDestroy()
        {            
            CharacterInventory.OnInventoryChanged -= OnInventoryChanged;
        }

        private void OnInventoryChanged(int index, Cell cellBefore, Cell cellAfter)
        {
            CharacterInventory.LogInventory();
        }
    }
}