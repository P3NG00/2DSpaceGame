using SpaceGame.Settings;
using SpaceGame.Utilities;
using UnityEngine;

namespace SpaceGame.Ships
{
    public sealed class ShipAI : Ship
    {
        [Header("Info [ShipAI]", order = 10)]
        [SerializeField] private ItemWeaponInfo weapon;
        [SerializeField] private Enums.ShipAIType shipAIType;

        private ShipAIInfo statsAI;
        private Ship target;

        private void Awake()
        {
            this.statsAI = (ShipAIInfo)this.Stats;
        }

        // TODO test ship ai
        protected override void FixedUpdate()
        {
            base.FixedUpdate();

            // If AI Type is 'Aggressive' or 'Stalk' (because they need tracking variables)
            if (this.shipAIType == Enums.ShipAIType.Aggressive || this.shipAIType == Enums.ShipAIType.Stalk)
            {
                Vector2 targetPos = this.target.Position;
                float distanceFromTarget = Vector2.Distance(this.Position, targetPos);
                bool withinDistance = distanceFromTarget < this.statsAI.DistanceStopFromTarget;

                if (this.shipAIType == Enums.ShipAIType.Aggressive)
                {
                    // Aggressive AI
                    bool withinFireDistance = distanceFromTarget < this.statsAI.DistanceFireAtTarget;
                    this.UpdateAI(withinFireDistance, withinDistance, !withinDistance, targetPos);
                }
                else
                {
                    // Stalk AI
                    this.UpdateAI(false, withinDistance, !withinDistance, targetPos);
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

        public override ItemWeaponInfo GetWeapon() => this.weapon;

        protected override void OnDeath()
        {
            Destroy(gameObject);
        }
    }
}
