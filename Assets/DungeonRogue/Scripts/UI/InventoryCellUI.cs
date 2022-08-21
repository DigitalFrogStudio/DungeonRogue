using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

namespace Assets.DungeonRogue.Scripts.UI
{
    public class InventoryCellUI : MonoBehaviour
    {
        [SerializeField]
        private Image iconImage = default;

        public void SetIcon(Sprite iconSprite)
        {
            iconImage.sprite = iconSprite;
        }

        private void Awake()
        {
            Assert.IsNotNull(iconImage);
        }
    }
}