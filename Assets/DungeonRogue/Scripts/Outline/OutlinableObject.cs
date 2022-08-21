using Assets.QuickOutline.Scripts;
using UnityEngine;
using UnityEngine.Assertions;

namespace Assets.DungeonRogue.Scripts
{
    public class OutlinableObject : MonoBehaviour
    {
        [SerializeField]
        private Outline outline = default;

        public void SetOutline(bool isOn)
        {
            outline.enabled = isOn;
        }

        private void Awake()
        {
            Assert.IsNotNull(outline);
        }

        private void Start()
        {
            outline.enabled = false;
        }
    }
}