using System.Collections.Generic;
using UnityEngine;

namespace Assets.DungeonRogue.Scripts.Items
{
    public static class ItemsDataStorage
    {
        private const string ITEM_DATA_SCRIPTABLE_OBJECTS_LOCATION = "ScriptableObjects/ItemsData";

        public static readonly Dictionary<int, GameObject> IDToItemPrefabDictionary;

        public static readonly Dictionary<int, ItemData> IDToItemDataDictionary;

        static ItemsDataStorage()
        {
            IDToItemPrefabDictionary = new Dictionary<int, GameObject>();
            IDToItemDataDictionary = new Dictionary<int, ItemData>();

            FillDictionary();
        }

        private static void FillDictionary()
        {
            List<ItemScriptableObject> itemsScriptableObjects =
                new List<ItemScriptableObject>(Resources.LoadAll<ItemScriptableObject>(ITEM_DATA_SCRIPTABLE_OBJECTS_LOCATION));

            foreach (ItemScriptableObject itemScriptableObject in itemsScriptableObjects)
            {
                ItemData data = itemScriptableObject.Data;
                IDToItemDataDictionary.Add(data.ID, data);

                GameObject prefab = itemScriptableObject.Prefab;
                IDToItemPrefabDictionary.Add(data.ID, prefab);
            }
        }
    }
}