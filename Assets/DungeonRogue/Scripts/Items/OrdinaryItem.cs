namespace Assets.DungeonRogue.Scripts.Items
{
    public class OrdinaryItem : IItem
    {
        private ItemData data;

        public OrdinaryItem(int id)
        {
            data = ItemsDataStorage.IDToItemDataDictionary[id];
        }

        public ItemData GetItemData()
        {
            return data;
        }
    }
}