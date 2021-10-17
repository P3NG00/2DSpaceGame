using SpaceGame.Settings;
using UnityEngine;

namespace SpaceGame.Items
{
    [System.Serializable]
    public sealed class ItemStack
    {
        [Header("ItemStack Info", order = 0)]
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
                this.FixStackSize();
            }
        }

        // Adds amount to stack, then returns
        // remaining amount that couldn't be added
        public int AddAmount(int amount)
        {
            this.amount += amount;
            return this.FixStackSize();
        }

        private int FixStackSize()
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
                this.Item = null;
            }

            return difference;
        }

        public override string ToString() => this.Item == null ? "Empty" : $"{this.Item.ItemName} - {this.amount}";
    }
}
