using SpaceGame.Ships;

namespace SpaceGame.Items
{
    public interface ItemUsable
    {
        void Use(Ship source);
    }
}