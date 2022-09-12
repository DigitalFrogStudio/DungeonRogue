using UnityEngine;
using UnityEngine.Assertions;

namespace Assets.DungeonRogue.Scripts.UI
{
    public class EquipmentUI : MonoBehaviour
    {
        [SerializeField]
        private MutualDraggedItemUI draggedItem = default;

        public MutualDraggedItemUI DraggedItem => draggedItem;

        private void Awake()
        {
            Assert.IsNotNull(draggedItem);
        }
    }
}