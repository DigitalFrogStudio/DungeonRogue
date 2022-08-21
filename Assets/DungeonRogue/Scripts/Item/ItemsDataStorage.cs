using System.Collections.Generic;

namespace Assets.DungeonRogue.Scripts
{
    public static class ItemsDataStorage
    {
        public static readonly Dictionary<int, ItemData> IDToItemDictionary;

        static ItemsDataStorage()
        {
            IDToItemDictionary = new Dictionary<int, ItemData>();

            FillDictionary();
        }

        private static void FillDictionary()
        {
            ItemData item;

            item = new ItemData(1, "Sword", 1);
            AddItem(item);

            item = new ItemData(2, "Long sword", 1);
            AddItem(item);

            item = new ItemData(3, "Axe", 1);
            AddItem(item);

            item = new ItemData(4, "Shield", 1);
            AddItem(item);
        }

        private static void AddItem(ItemData data)
        {            
            IDToItemDictionary.Add(data.ID, data);
        }
    }
}