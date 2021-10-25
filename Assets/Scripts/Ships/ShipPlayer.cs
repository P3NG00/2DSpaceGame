using SpaceGame.Items;
using UnityEngine;

namespace SpaceGame.Ships
{
    public sealed class ShipPlayer : Ship
    {
        [Header("References [ShipPlayer]", order = 99)]
        [SerializeField] private Animator animatorPlayer;

        public Animator Animator => this.animatorPlayer;

        public override ItemProjectileInfo GetProjectile() => GameInfo.PlayerWeapon;

        // protected override void OnDeath() { }
    }
}
