using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Assets.DungeonRogue.Scripts.UI
{
    public abstract class ItemCellUI : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler, IDropHandler
    {
        public event Action<ItemCellUI, ItemCellUI> OnItemDroppedIntoCell;

        public event Action<ItemCellUI> OnItemToBeDroppedIntoWorld;

        [SerializeField]
        private Image cellImage = default;

        [SerializeField]
        private Image iconImage = default;

        [SerializeField]
        private Color selectedColor = default;

        private MutualDraggedItemUI draggedItemUI;

        private Canvas canvas;

        private Color initialCellColor;

        private bool isCellEngagedMutualItem;

        private bool CanBeginDrag => (iconImage.enabled == true) &&
                                  (draggedItemUI.IsEngaged == false);

        public abstract MutualDraggedItemUI GetDraggedItemUI();

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

            ItemCellUI sourceIntemCell = eventData.pointerDrag.GetComponent<ItemCellUI>();
            if (sourceIntemCell == null)
            {
                return;
            }

            OnItemDroppedIntoCell(sourceIntemCell, this);
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

        virtual protected void Awake()
        {
            Assert.IsNotNull(cellImage);
            Assert.IsNotNull(iconImage);

            canvas = GetComponentInParent<Canvas>();
            Assert.IsNotNull(canvas);

            iconImage.sprite = null;
        }

        virtual protected void Start()
        {
            initialCellColor = cellImage.color;

            draggedItemUI = GetDraggedItemUI();

            SetIcon(null);
        }
    }
}