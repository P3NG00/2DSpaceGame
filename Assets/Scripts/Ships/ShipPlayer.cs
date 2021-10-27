using SpaceGame.Items;
using SpaceGame.UI;
using UnityEngine;

namespace SpaceGame.Ships
{
    public sealed class ShipPlayer : Ship
    {
        [Header("References [ShipPlayer]", order = 99)]
        [SerializeField] private Animator animatorPlayer;

        public Animator Animator => this.animatorPlayer;

        public sealed override UIInventorySlot ItemWeaponSlot() => GameInfo.SlotWeapon;
        public sealed override ItemInfoProjectile GetProjectile() => GameInfo.PlayerWeaponInfo;

        // protected override void OnDeath() { }
    }
}
