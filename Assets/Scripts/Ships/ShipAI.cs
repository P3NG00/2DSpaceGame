using SpaceGame.Settings;
using UnityEngine;

namespace SpaceGame.Ships
{
    public sealed class ShipAI : Ship
    {
        private ShipAIStats stats;

        private void Awake() => stats = (ShipAIStats)this.Stats;

        private void FixedUpdate()
        {
            float distance = Vector2.Distance(this.Position, GameInfo.Player.Position);

            if (distance < this.stats.DistanceStopFromPlayer)
            {
                this.ApplyDrag(true);
            }
            else
            {
                this.ApplyDrag(false);
                this.ApplyForce();
            }

            this.RotateToLookAt(GameInfo.Player.Position);
        }

        protected override void OnDeath()
        {
            Destroy(gameObject);
        }
    }
}
