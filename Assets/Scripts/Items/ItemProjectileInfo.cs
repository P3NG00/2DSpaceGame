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

        public sealed override void Use(Ship source)
        {
            // TODO spawn projectile
            // make rock chunks an ItemProjectile for testing
            // also make missiles into projetiles
            GameInfo.SpawnProjectileObject(projectile, source);
        }
    }
}
