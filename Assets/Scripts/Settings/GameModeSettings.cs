using UnityEngine;
using UnityEngine.Serialization;

namespace SpaceGame.Settings
{
    [CreateAssetMenu(menuName = "2D Space Game/Game Mode Settings", fileName = "GameMode Settings")]
    public sealed class GameModeSettings : ScriptableObject
    {
        [Header("Space Rocks", order = 40)]
        [SerializeField, Min(0f)] private float sr_minVelocity;
        [SerializeField] private float sr_maxVelocity;
        [SerializeField, Min(0f)] private float sr_minAngularVelocity;
        [SerializeField] private float sr_maxAngularVelocity;
        [SerializeField, Min(0f)] private float sr_minScale;
        [SerializeField] private float sr_maxScale;
        [SerializeField] private float sr_scaleSpawnRate;
        [SerializeField] private float sr_distanceSpawn;
        [SerializeField] private float sr_distanceMax;
        [SerializeField] private float sr_timeBetweenCleanup;
        [SerializeField, Min(0f)] private float sr_timeBetweenChance;
        [SerializeField, Range(0f, 1f)] private float sr_chanceSpawn;
        [SerializeField, Min(0f)] private float sr_scaleMissileImpactForce;

        private void OnValidate()
        {
            void ValidateMinMax(float min, ref float max) { if (min > max) { max = min; } }
            ValidateMinMax(sr_minVelocity, ref sr_maxVelocity);
            ValidateMinMax(sr_minAngularVelocity, ref sr_maxAngularVelocity);
            ValidateMinMax(sr_minScale, ref sr_maxScale);
        }

        public float MinSpaceRockVelocity => sr_minVelocity;
        public float MaxSpaceRockVelocity => sr_maxVelocity;
        public float MinSpaceRockVelocityAngular => sr_minAngularVelocity;
        public float MaxSpaceRockVelocityAngular => sr_maxAngularVelocity;
        public float MinSpaceRockScale => sr_minScale;
        public float MaxSpaceRockScale => sr_maxScale;
        public float ScaleSpaceRockSpawnRate => sr_scaleSpawnRate;
        public float DistanceSpaceRockSpawn => sr_distanceSpawn;
        public float DistanceSpaceRockMax => sr_distanceMax;
        public float TimeBetweenSpaceRockCleanup => sr_timeBetweenCleanup;
        public float TimeBetweenSpaceRockChance => sr_timeBetweenChance;
        public float ChanceSpaceRockSpawn => sr_chanceSpawn;
        public float ScaleMissileImpactForce => sr_scaleMissileImpactForce;
    }
}
