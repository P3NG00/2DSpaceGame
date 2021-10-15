using SpaceGame.Settings;
using UnityEngine;

namespace SpaceGame.Items
{
    [System.Serializable]
    public sealed class ItemStack
    {
        public Item Item;
        [SerializeField, Min(0)] private int amount;

        public ItemStack(Item item, int amount)
        {
            this.Item = item;
            this.amount = amount;
        }

        public int Amount
        {
            get => this.amount;
            set
            {
                this.amount = value;
                this.ValidateStackSize();
            }
        }

        // Adds amount to stack, then returns
        // remaining amount that couldn't be added
        public int AddAmount(int amount)
        {
            this.amount += amount;
            return this.ValidateStackSize();
        }

        private int ValidateStackSize()
        {
            int difference = 0, maxStackSize = this.Item.MaxStackSize;

            if (this.amount > maxStackSize)
            {
                difference = this.amount - maxStackSize;
                this.amount = maxStackSize;
            }
            else if (this.amount < 0)
            {
                this.amount = 0;
            }

            return difference;
        }
    }
}
