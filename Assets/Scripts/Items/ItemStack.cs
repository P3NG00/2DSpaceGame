using System.Globalization;
using SpaceGame.Settings;
using UnityEngine;

namespace SpaceGame.Items
{
    [System.Serializable]
    public sealed class ItemStack
    {
        public Item Item;
        [SerializeField] private int amount;

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
                this.CheckStackSize();
            }
        }

        public int AddAmount(int amount)
        {
            this.amount += amount;
            return this.CheckStackSize();
        }

        private int CheckStackSize()
        {
            int difference = 0, maxStackSize = this.Item.MaxStackSize;

            if (this.amount > maxStackSize)
            {
                difference = this.amount - maxStackSize;
                this.amount = maxStackSize;
            }

            return difference;
        }
    }
}
