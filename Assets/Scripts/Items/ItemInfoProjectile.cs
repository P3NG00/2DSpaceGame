using SpaceGame.Projectiles;
using SpaceGame.Ships;
using UnityEngine;
using UnityEngine.Serialization;

namespace SpaceGame.Items
{
    [CreateAssetMenu(menuName = "2D Space Game/Item/Projectile", fileName = "Item Projectile")]
    public sealed class ItemInfoProjectle : ItemInfo
    {
        [Header("Info [ItemProjectile]", order = 5)]
        // TODO remove FormerlySerializedAs tag
        [SerializeField, FormerlySerializedAs("projectile")] private ProjectileInfo projectile;
        [SerializeField] private float timeBetweenShots;

        public ProjectileInfo ProjectileInfo => this.projectile;
        public float TimeBetweenShots => this.timeBetweenShots;

        public sealed override void Use(Ship source) => Projectile.Create(projectile, source);
    }
}
