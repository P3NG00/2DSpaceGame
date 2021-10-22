using UnityEngine;

namespace SpaceGame.Settings
{
    [CreateAssetMenu(menuName = "2D Space Game/Ship Stats/AI", fileName = "Ship AI Info")]
    public sealed class ShipAIInfo : ShipInfo
    {
        [Header("Info [ShipAIStats]", order = 5)]
        [SerializeField] private float distanceStopFromTarget;
        [SerializeField] private float distanceFireAtTarget;

        public float DistanceStopFromTarget => this.distanceStopFromTarget;
        public float DistanceFireAtTarget => this.distanceFireAtTarget;
    }
}
