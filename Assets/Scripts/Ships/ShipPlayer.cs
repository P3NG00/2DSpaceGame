using UnityEngine;

namespace SpaceGame.Ships
{
    public sealed class ShipPlayer : Ship
    {
        [Header("References (as ShipPlayer)", order = 99)]
        [SerializeField] private Animator animator;

        public Animator Animator => animator;

        protected override void OnDeath()
        {
            // TODO on player death
        }
    }
}
