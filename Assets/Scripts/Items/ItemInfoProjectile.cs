using System.Diagnostics;
using System.Collections;
using SpaceGame.Projectiles;
using SpaceGame.Ships;
using UnityEngine;

namespace SpaceGame.Items
{
    [CreateAssetMenu(menuName = "2D Space Game/Item/Projectile", fileName = "Item Projectile")]
    public sealed class ItemInfoProjectile : ItemInfoWeapon
    {
        [Header("Info [ItemInfoProjectile]", order = 5)]
        [SerializeField] private ProjectileInfo projectile;

        public sealed override GameObject UseWeapon(Ship source)
        {
            Projectile.Create(this.projectile, source);
            return null;
        }

        public sealed override IEnumerator UseRoutine(Ship ship)
        {
            ItemInfo itemInfo = ship.GetWeapon();

            while (ship.IsFiring && itemInfo != null)
            {
                ship.UseWeapon();
                yield return new WaitForSeconds(itemInfo.TimeBetweenUses);
                itemInfo = ship.GetWeapon();
            }

            ship.IsFiring = false;
        }
    }
}
