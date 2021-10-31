using System.Collections;
using SpaceGame.Lazers;
using SpaceGame.Ships;
using UnityEngine;

namespace SpaceGame.Items
{
    [CreateAssetMenu(menuName = "2D Space Game/Item/Lazer", fileName = "Item Info Lazer")]
    public sealed class ItemInfoLazer : ItemInfo, ItemWeapon
    {
        [Header("Info [ItemInfoLazer]", order = 5)]
        [SerializeField] private LazerInfo lazerInfo;

        public GameObject UseWeapon(Ship source) => Lazer.Create(this.lazerInfo, source).gameObject;

        public IEnumerator UseRoutine(Ship ship)
        {
            ItemInfo itemInfo = ship.GetItemInfo();
            ship.UseWeapon();

            while (ship.IsFiring && itemInfo != null)
            {
                yield return new WaitForFixedUpdate();
            }

            ship.IsFiring = false;
        }
    }
}
