using SpaceGame.Settings;

namespace SpaceGame.Items
{
    [System.Serializable]
    public sealed class ItemStack
    {
        public ItemStack(ItemInfo item, int amount)
        {
            this.Item = item;
            this.Amount = amount;
        }

        public ItemStack(ItemInfo item) : this(item, 1) { }
        public ItemStack() : this(null, 0) { }

        public ItemInfo Item;
        public int Amount;
    }
}