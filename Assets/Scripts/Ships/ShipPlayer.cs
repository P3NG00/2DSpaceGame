using SpaceGame.Settings;
using UnityEngine;

namespace SpaceGame.Ships
{
    public sealed class ShipPlayer : Ship
    {
        [Header("References [ShipPlayer]", order = 99)]
        [SerializeField] private Animator animator;

        public Animator Animator => this.animator;

        public override Weapon GetWeapon() => GameInfo.PlayerWeapon;

        protected override void OnDeath()
        {
            // TODO on player death
        }
    }
}
