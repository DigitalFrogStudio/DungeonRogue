using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Assets.DungeonRogue.Scripts.UI
{
    public class InventoryCellUI : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler, IDropHandler
    {
        public event Action<InventoryCellUI, InventoryCellUI> OnItemDroppedIntoCell;

        public event Action<InventoryCellUI> OnItemToBeDroppedIntoWorld;

        [SerializeField]
        private Image cellImage = default;

        [SerializeField]
        private Image iconImage = default;

        [SerializeField]
        private Color selectedColor = default;

        private InventoryUI inventoryUI;

        private MutualDraggedItemUI draggedItemUI;

        private Canvas canvas;

        private Color initialCellColor;

        private bool isCellEngagedMutualItem;

        private bool CanBeginDrag => (iconImage.enabled == true) && 
                                  (draggedItemUI.IsEngaged == false);

        public void OnBeginDrag(PointerEventData eventData)
        {
            if (CanBeginDrag == false)
            {
                return;
            }

            isCellEngagedMutualItem = true;

            cellImage.color = selectedColor;

            draggedItemUI.SetIcon(iconImage.sprite);

            draggedItemUI.IconRectTransform.position = iconImage.rectTransform.position;
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (isCellEngagedMutualItem == false)
            {
                return;
            }

            draggedItemUI.IconRectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            if (isCellEngagedMutualItem == false)
            {
                return;
            }


            isCellEngagedMutualItem = false;

            cellImage.color = initialCellColor;

            draggedItemUI.SetIcon(null);

            List<RaycastResult> raycastResults = new List<RaycastResult>();
            EventSystem.current.RaycastAll(eventData, raycastResults);

            if (raycastResults.Count == 0)
            {
                OnItemToBeDroppedIntoWorld?.Invoke(this);
            }
        }

        public void OnDrop(PointerEventData eventData)
        {
            if (draggedItemUI.IsEngaged == false)
            {
                return;
            }

            InventoryCellUI sourceInventoryCell = eventData.pointerDrag.GetComponent<InventoryCellUI>();
            if (sourceInventoryCell == null)
            {
                return;
            }

            OnItemDroppedIntoCell(sourceInventoryCell, this);
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
        }

        private void Start()
        {
            initialCellColor = cellImage.color;

            draggedItemUI = inventoryUI.DraggedItem;

            SetIcon(null);
        }
    }
}