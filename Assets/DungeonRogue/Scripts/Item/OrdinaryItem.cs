namespace Assets.DungeonRogue.Scripts
{
    public class OrdinaryItem : IItem
    {
        private ItemData data;

        public OrdinaryItem(int id)
        {
            data = ItemsDataStorage.IDToItemDictionary[id];
        }

        public ItemData GetItemData()
        {
            return data;
        }
    }
}