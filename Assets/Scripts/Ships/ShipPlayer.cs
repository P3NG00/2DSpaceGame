using SpaceGame.Settings;
using UnityEngine;

namespace SpaceGame.Ships
{
    public sealed class ShipPlayer : Ship
    {
        [Header("References [ShipPlayer]", order = 99)]
        [SerializeField] private Animator animatorPlayer;

        public Animator Animator => this.animatorPlayer;

        public override ItemWeaponInfo GetWeapon() => GameInfo.PlayerWeapon;

        // TODO protected override void OnDeath() { }
    }
}
