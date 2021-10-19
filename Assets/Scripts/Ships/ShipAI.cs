using SpaceGame.Settings;
using SpaceGame.Utilities;
using UnityEngine;

namespace SpaceGame.Ships
{
    public sealed class ShipAI : Ship
    {
        [Header("Info [ShipAI]", order = 10)]
        [SerializeField] private Weapon weapon;
        [SerializeField] private Enums.ShipAIType shipAIType;

        private ShipAIStats statsAI;

        private void Awake()
        {
            this.statsAI = (ShipAIStats)this.Stats;
        }

        private void FixedUpdate()
        {
            Vector2 playerPos = GameInfo.Player.Position;
            bool withinDistance = Vector2.Distance(this.Position, playerPos) < this.statsAI.DistanceStopFromPlayer;

            switch (this.shipAIType)
            {
                case Enums.ShipAIType.Aggressive: this.ShipAI_Aggressive(playerPos, withinDistance); break;
                case Enums.ShipAIType.Passive: this.ShipAI_Passive(); break;
                case Enums.ShipAIType.Stalk: this.ShipAI_Stalk(playerPos, withinDistance); break;
            }
        }

        private void ShipAI_Passive()
        {
            this.IsFiring = false;
            this.ApplyForce();
        }

        private void ShipAI_Aggressive(Vector2 target, bool withinDistance)
        {
            this.RotateToLookAt(target);
            this.IsFiring = true;
            this.ApplyDrag(withinDistance);

            if (!withinDistance)
            {
                this.ApplyForce();
            }
        }

        private void ShipAI_Stalk(Vector2 target, bool withinDistance)
        {
            this.RotateToLookAt(target);
            this.IsFiring = false;
            this.ApplyDrag(withinDistance);

            if (!withinDistance)
            {
                this.ApplyForce();
            }
        }

        public override Weapon GetWeapon() => this.weapon;

        protected override void OnDeath()
        {
            Destroy(gameObject);
        }
    }
}
