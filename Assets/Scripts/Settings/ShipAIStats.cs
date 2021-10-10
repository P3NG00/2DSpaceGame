using UnityEngine;

namespace SpaceGame.Settings
{
    [CreateAssetMenu(menuName = "2D Space Game/Ship Stats/AI", fileName = "Ship AI Stats")]
    public sealed class ShipAIStats : ShipStats
    {
        [Header("Info [ShipAIStats]", order = 5)]
        [SerializeField] private float distanceStopFromPlayer;

        public float DistanceStopFromPlayer => this.distanceStopFromPlayer;
    }
}
