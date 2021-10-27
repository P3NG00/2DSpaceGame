using UnityEngine;

namespace SpaceGame.Items
{
    [System.Serializable]
    public sealed class ItemStack
    {
        [Header("ItemStack Info", order = 0)]
        public ItemInfo ItemInfo;
        [Min(0)] public int Amount;

        public ItemStack(ItemInfo itemInfo, int amount)
        {
            this.ItemInfo = itemInfo;
            this.Amount = amount;
            this.ModifyAmount(0);
        }

        public ItemStack(ItemInfo itemInfo) : this(itemInfo, 1) { }

        public int ModifyAmount(int amount)
        {
            this.Amount += amount;
            int difference = 0;
            int stackMax = this.ItemInfo.MaxStackSize;

            if (this.Amount < 0)
            {
                // If stack is empty
                difference = this.Amount;
                this.Amount = 0;
                this.ItemInfo = null;
            }
            else if (this.Amount > stackMax)
            {
                // If stack exceeds limit
                difference = this.Amount - stackMax;
                this.Amount = stackMax;
            }

            return difference;
        }

        public override string ToString() => this.ItemInfo == null ? "Empty" : $"{this.ItemInfo.ItemName} - {this.Amount}";
    }
}
