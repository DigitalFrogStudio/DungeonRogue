using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Assets.DungeonRogue.Scripts.UI
{
    public class InventoryCellUI : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
    {        
        [SerializeField]
        private Image cellImage = default;

        [SerializeField]
        private Image iconImage = default;

        [SerializeField]
        private Color selectedColor = default;

        private InventoryUI inventoryUI;

        private MutualDraggedItemUI draggedItem;

        private Canvas canvas;

        private Color initialCellColor;

        private Vector2 initialIconAnchoredPosition;

        private bool isCellEngagedMutualItem;

        private bool CanBeginDrag => (iconImage.enabled == true) && 
                                  (draggedItem.IsEngaged == false);

        public void OnBeginDrag(PointerEventData eventData)
        {
            if (CanBeginDrag == false)
            {
                return;
            }

            isCellEngagedMutualItem = true;

            cellImage.color = selectedColor;

            draggedItem.SetIcon(iconImage.sprite);

            draggedItem.IconRectTransform.position = iconImage.rectTransform.position;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            if (isCellEngagedMutualItem == false)
            {
                return;
            }

            isCellEngagedMutualItem = false;

            cellImage.color = initialCellColor;

            draggedItem.SetIcon(null);
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (isCellEngagedMutualItem == false)
            {
                return;
            }

            draggedItem.IconRectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
        }

        public void SetIcon(Sprite iconSprite)
        {
            iconImage.sprite = iconSprite;

            if (iconSprite == null)
            {
                iconImage.enabled = false;
            }
            else
            {                
                iconImage.enabled = true;
            }
        }

        private void Awake()
        {
            Assert.IsNotNull(cellImage);
            Assert.IsNotNull(iconImage);

            canvas = GetComponentInParent<Canvas>();
            Assert.IsNotNull(canvas);

            inventoryUI = GetComponentInParent<InventoryUI>();
            Assert.IsNotNull(inventoryUI);

            iconImage.sprite = null;

            initialIconAnchoredPosition = iconImage.rectTransform.anchoredPosition;
        }

        private void Start()
        {
            initialCellColor = cellImage.color;

            draggedItem = inventoryUI.DraggedItem;

            SetIcon(null);
        }
    }
}