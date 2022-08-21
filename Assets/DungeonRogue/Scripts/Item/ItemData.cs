namespace Assets.DungeonRogue.Scripts
{
    public struct ItemData
    {
        public int ID { get; private set; }

        public string Name { get; private set; }

        public int LimitPerInventoryCell { get; private set; }

        public ItemData(int id, string name, int limitPerInventoryCell)
        {
            ID = id;

            Name = name;

            LimitPerInventoryCell = limitPerInventoryCell;
        }
    }
}