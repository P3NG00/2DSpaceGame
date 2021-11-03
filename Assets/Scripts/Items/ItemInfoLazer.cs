using System.Collections;
using SpaceGame.Lazers;
using SpaceGame.Ships;
using UnityEngine;

namespace SpaceGame.Items
{
    [CreateAssetMenu(menuName = "2D Space Game/Item/Lazer", fileName = "Item Info Lazer")]
    public sealed class ItemInfoLazer : ItemInfoWeapon
    {
        [Header("Info [ItemInfoLazer]", order = 5)]
        [SerializeField] private LazerInfo lazerInfo;

        public sealed override GameObject UseWeapon(Ship source) => Lazer.Create(this.lazerInfo, source).gameObject;

        public sealed override IEnumerator UseRoutine(Ship ship)
        {
            ItemInfo itemInfo = ship.GetWeapon();
            ship.UseWeapon();

            while (ship.IsFiring && itemInfo != null)
            {
                yield return new WaitForFixedUpdate();
            }

            ship.IsFiring = false;
        }
    }
}
