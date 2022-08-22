using System;
using UnityEngine;

namespace Assets.DungeonRogue.Scripts
{
    [Serializable]
    public struct ItemData
    {
        [SerializeField]
        private int id;

        [SerializeField]
        private string name;

        [SerializeField]
        private int limitPerInventoryCell;

        [SerializeField]
        private Sprite icon;

        public int ID => id;

        public string Name => name;

        public int LimitPerInventoryCell => limitPerInventoryCell;

        public Sprite Icon => icon;
    }
}