using SpaceGame.Ships;
using UnityEngine;

namespace SpaceGame.Items
{
    [CreateAssetMenu(menuName = "2D Space Game/Item/Health")]
    public sealed class ItemInfoHealth : ItemInfoUsable
    {
        [Header("Info [ItemInfoHealth]", order = 5)]
        [SerializeField] private float amount;

        public sealed override void Use(Ship source) => source.Heal(this.amount);
    }
}