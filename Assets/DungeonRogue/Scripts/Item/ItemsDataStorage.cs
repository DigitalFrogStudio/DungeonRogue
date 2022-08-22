using Assets.DungeonRogue.Scripts.Item;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.DungeonRogue.Scripts
{
    public static class ItemsDataStorage
    {
        private const string ITEM_DATA_SCRIPTABLE_OBJECTS_LOCATION = "ScriptableObjects/ItemsData";

        public static readonly Dictionary<int, ItemData> IDToItemDictionary;

        static ItemsDataStorage()
        {
            IDToItemDictionary = new Dictionary<int, ItemData>();

            FillDictionary();
        }

        private static void FillDictionary()
        {
            List<ItemDataScriptableObject> itemsDataRaw =
                new List<ItemDataScriptableObject>(Resources.LoadAll<ItemDataScriptableObject>(ITEM_DATA_SCRIPTABLE_OBJECTS_LOCATION));

            foreach (ItemDataScriptableObject rawItem in itemsDataRaw)
            {
                ItemData data = rawItem.Data;

                IDToItemDictionary.Add(data.ID, data);
            }
        }
    }
}