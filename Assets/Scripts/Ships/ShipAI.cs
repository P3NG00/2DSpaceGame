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

        // TODO test ship ai
        private void FixedUpdate()
        {
            // If AI Type is 'Aggressive' or 'Stalk' (because they need tracking variables)
            if (this.shipAIType == Enums.ShipAIType.Aggressive || this.shipAIType == Enums.ShipAIType.Stalk)
            {
                // TODO change playerPos to 'target' ship
                Vector2 target = GameInfo.Player.Position;
                bool withinDistance = Vector2.Distance(this.Position, target) < this.statsAI.DistanceStopFromPlayer;

                if (this.shipAIType == Enums.ShipAIType.Aggressive)
                {
                    // Aggressive AI
                    // TODO add firing distance
                    this.UpdateAI(true, withinDistance, !withinDistance, target);
                }
                else
                {
                    // Stalk AI
                    this.UpdateAI(false, withinDistance, !withinDistance, target);
                }
            }
            // If AI Type is 'Passive' (does not need tracking variables)
            else
            {
                // Passive AI
                this.UpdateAI(false, false, true);
            }
        }

        private void UpdateAI(bool firing, bool drag, bool applyForce, Vector2? target = null)
        {
            if (target != null)
            {
                this.RotateToLookAt((Vector2)target);
            }

            if (!drag & applyForce)
            {
                this.ApplyForce();
            }

            this.IsFiring = firing;
            this.ApplyDrag(drag);
        }

        public override Weapon GetWeapon() => this.weapon;

        protected override void OnDeath()
        {
            Destroy(gameObject);
        }
    }
}
