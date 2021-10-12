using SpaceGame.Settings;
using SpaceGame.Utilities;
using UnityEngine;

namespace SpaceGame.Ships
{
    public sealed class ShipAI : Ship
    {
        [Header("Info [ShipAI]", order = 10)]
        [SerializeField] private ShipAIType shipAIType;

        private ShipAIStats statsAI;

        private void Awake()
        {
            this.statsAI = (ShipAIStats)this.Stats;
        }

        private void FixedUpdate()
        {
            Vector2 playerPos = GameInfo.Player.Position;
            float distance = Vector2.Distance(this.Position, playerPos);
            bool withinDistance = distance < this.statsAI.DistanceStopFromPlayer;

            switch (this.shipAIType)
            {
                case ShipAIType.Aggressive: this.ShipAI_Aggressive(playerPos, withinDistance); break;
                case ShipAIType.Passive: this.ShipAI_Passive(); break;
                case ShipAIType.Stalk: this.ShipAI_Stalk(playerPos, withinDistance); break;
            }
        }

        private void ShipAI_Passive()
        {
            this.IsFiring = false;
            this.ApplyForce();
        }

        private void ShipAI_Aggressive(Vector2 playerPos, bool withinDistance)
        {
            this.RotateToLookAt(playerPos);
            this.IsFiring = true;
            this.ApplyDrag(withinDistance);

            if (!withinDistance)
            {
                this.ApplyForce();
            }
        }

        private void ShipAI_Stalk(Vector2 playerPos, bool withinDistance)
        {
            this.RotateToLookAt(playerPos);
            this.IsFiring = false;
            this.ApplyDrag(withinDistance);

            if (!withinDistance)
            {
                this.ApplyForce();
            }
        }

        protected override void OnDeath()
        {
            Destroy(gameObject);
        }

        private enum ShipAIType
        {
            Passive,
            Aggressive,
            Stalk,
        }
    }
}
