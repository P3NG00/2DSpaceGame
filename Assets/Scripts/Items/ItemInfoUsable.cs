using SpaceGame.Ships;

namespace SpaceGame.Items
{
    public abstract class ItemInfoUsable : ItemInfo
    {
        public abstract void Use(Ship source);
    }
}