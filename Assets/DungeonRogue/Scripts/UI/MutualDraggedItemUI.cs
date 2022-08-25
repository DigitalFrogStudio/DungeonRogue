using UnityEngine;
using UnityEngine.UI;

namespace Assets.DungeonRogue.Scripts.UI
{
    public class MutualDraggedItemUI : MonoBehaviour
    {        
        [SerializeField]
        private Image iconImage = default;

        public RectTransform IconRectTransform => iconImage.rectTransform;

        public bool IsEngaged => iconImage.enabled;
        
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
        private void Start()
        {
            SetIcon(null);
        }
    }
}