using SpaceGame.Projectiles;
using SpaceGame.Settings;
using SpaceGame.Ships;
using UnityEngine;

namespace SpaceGame.Items
{
    [CreateAssetMenu(menuName = "2D Space Game/Item/Projectile", fileName = "Item Projectile")]
    public sealed class ItemProjectileInfo : ItemInfo
    {
        [Header("Info [ItemProjectile]", order = 5)]
        [SerializeField] private ProjectileInfo projectile;
        [SerializeField] private float timeBetweenShots;

        public float TimeBetweenShots => this.timeBetweenShots;

        public sealed override void Use(Ship source) => GameInfo.SpawnProjectileObject(projectile, source);
    }
}
