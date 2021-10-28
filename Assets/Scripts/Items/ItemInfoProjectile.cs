using SpaceGame.Projectiles;
using SpaceGame.Ships;
using UnityEngine;

namespace SpaceGame.Items
{
    [CreateAssetMenu(menuName = "2D Space Game/Item/Projectile", fileName = "Item Projectile")]
    public sealed class ItemInfoProjectile : ItemInfo
    {
        [Header("Info [ItemProjectile]", order = 5)]
        [SerializeField] private ProjectileInfo projectile;
        [SerializeField] private float timeBetweenShots;

        public ProjectileInfo ProjectileInfo => this.projectile;
        public float TimeBetweenShots => this.timeBetweenShots; // TODO move up to ItemInfo and implement for all usable items

        public sealed override void Use(Ship source) => Projectile.Create(projectile, source);
    }
}
