using SpaceGame.Items;
using SpaceGame.Utilities;
using UnityEngine;

namespace SpaceGame.Ships
{
    public sealed class ShipAI : Ship
    {
        [Header("Info [ShipAI]", order = 10)]
        [SerializeField] private ItemInfoWeapon itemWeapon;
        [SerializeField] private ItemInfoDefense itemDefense;
        [SerializeField] private Enums.ShipAIType shipAIType;
        [SerializeField] private bool randomAIType;

        [Header("Debug [ShipAI]", order = 105)]
        [SerializeField] private Ship target;

        private ShipAIInfo shipInfoAI;

        protected sealed override void Awake()
        {
            base.Awake();
            this.shipInfoAI = (ShipAIInfo)this.ShipInfo;

            if (this.randomAIType)
            {
                this.shipAIType = Util.RandomEnum<Enums.ShipAIType>();
            }
        }

        protected override void FixedUpdate()
        {
            base.FixedUpdate();

            // If AI Type is 'Aggressive' or 'Stalk' (because they need tracking variables)
            if (this.shipAIType == Enums.ShipAIType.Aggressive || this.shipAIType == Enums.ShipAIType.Stalk)
            {
                Vector2 targetPos = this.target.Position;
                float distanceFromTarget = Vector2.Distance(this.Position, targetPos);
                bool withinDistance = distanceFromTarget < this.shipInfoAI.DistanceStopFromTarget;

                if (this.shipAIType == Enums.ShipAIType.Aggressive)
                {
                    // Aggressive AI
                    this.UpdateAI(distanceFromTarget < this.shipInfoAI.DistanceFireAtTarget, withinDistance, !withinDistance, targetPos);
                }
                else // (this.shipType == Enums.ShipAIType.Stalk)
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

        public sealed override ItemInfoWeapon GetWeapon() => this.itemWeapon;
        public sealed override ItemInfoDefense GetDefense() => this.itemDefense;

        protected override void OnDeath()
        {
            Destroy(gameObject);
        }
    }
}
