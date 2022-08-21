using UnityEngine;

namespace Assets.DungeonRogue.Scripts
{
    public struct Cell
    {

        private int amount;

        public int Amount
        {
            get
            {
                return amount;
            }

            set
            {
                if (value < 0)
                {
                    Debug.LogWarning("Try to assign negative value to an amount variable in inventory");

                    amount = 0;

                    return;
                }
                else if (value == 0)
                {
                    amount = value;

                    StoredItem = default;
                }
                else
                {
                    amount = value;
                }
            }
        }

        public ItemData StoredItem { get; set; }

        public bool IsEmpty
        {
            get
            {

                if (Amount == 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }


        public Cell(int amount, ItemData itemData) : this()
        {
            Amount = amount;

            StoredItem = itemData;
        }
    }
}