using SpaceGame.Ships;
using UnityEngine;

namespace SpaceGame.Items
{
    [CreateAssetMenu(menuName = "2D Space Game/Item/Health")]
    public sealed class ItemInfoHealth : ItemInfo, ItemUsable
    {
        [Header("Info [ItemInfoHealth]", order = 5)]
        [SerializeField] private float amount;

        public void Use(Ship source) => source.Heal(this.amount);
    }
}