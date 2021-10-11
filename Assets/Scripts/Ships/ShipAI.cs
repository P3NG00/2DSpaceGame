using SpaceGame.Settings;
using SpaceGame.Utilities;
using UnityEngine;

namespace SpaceGame.Ships
{
    public sealed class ShipAI : Ship
    {
        [Header("DEBUG [ShipAI]", order = 110)]
        [SerializeField] private ShipAIType shipAIType; // TODO move to scriptable object? (reassess after working on getting mechanic working)

        private ShipAIStats statsAI;

        private void Awake()
        {
            this.statsAI = (ShipAIStats)this.Stats;
            this.shipAIType = Util.RandomEnum<ShipAIType>();
        }

        private void FixedUpdate()
        {
            Vector2 playerPos = GameInfo.Player.Position;
            float distance = Vector2.Distance(this.Position, playerPos);
            bool withinDistance = distance < this.statsAI.DistanceStopFromPlayer;

            switch (this.shipAIType)
            {
                case ShipAIType.Enemy:
                    this.RotateToLookAt(playerPos);
                    this.IsFiring = true;
                    this.ApplyDrag(withinDistance);

                    if (!withinDistance)
                    {
                        this.ApplyForce();
                    }
                    break;

                case ShipAIType.Passive:
                    this.ApplyForce();
                    this.IsFiring = false;
                    break;

                case ShipAIType.TalkToPlayer:
                    this.RotateToLookAt(playerPos);
                    this.IsFiring = false;
                    this.ApplyDrag(withinDistance);

                    if (!withinDistance)
                    {
                        this.ApplyForce();
                    }
                    break;
            }

        }

        protected override void OnDeath()
        {
            Destroy(gameObject);
        }

        private enum ShipAIType
        {
            Passive,
            Enemy,
            TalkToPlayer,
        }
    }
}
