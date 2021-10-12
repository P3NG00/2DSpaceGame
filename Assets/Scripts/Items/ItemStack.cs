using SpaceGame.Settings;

namespace SpaceGame.Items
{
    [System.Serializable]
    public sealed class ItemStack
    {
        public ItemStack(Item item, int amount)
        {
            this.Item = item;
            this.Amount = amount;
        }

        public ItemStack(Item item) : this(item, 1) { }
        public ItemStack() : this(null, 0) { }

        public Item Item;
        public int Amount;
    }
}