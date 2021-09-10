using UnityEngine;
using UnityEngine.Serialization;

namespace Project
{
    [CreateAssetMenu(menuName = "Project/Game Mode Settings", fileName = "GameMode Settings")]
    public sealed class GameModeSettings : ScriptableObject
    {
        [Header("Ships", order = 30)]
        [SerializeField] private ShipStats shipPlayer;
        [SerializeField] private ShipStats shipEnemy;

        [Header("Space Rocks", order = 40)]
        [SerializeField, Min(0f)] private float minVelocity;
        [SerializeField] private float maxVelocity;
        [SerializeField, Min(0f)] private float minAngularVelocity;
        [SerializeField] private float maxAngularVelocity;
        [SerializeField, Min(0f)] private float minScale;
        [SerializeField] private float maxScale;
        [SerializeField] private float scaleSpawnRate;
        [SerializeField] private float distanceSpawn;
        [SerializeField] private float distanceMax;
        [SerializeField] private float timeBetweenCleanup;
        [SerializeField, Min(0f)] private float timeBetweenChance;
        [SerializeField, Range(0f, 1f)] private float chanceSpawn;
        [SerializeField, Min(0f)] private float scaleMissileImpactForce;
        private void OnValidate()
        {
            void ValidateMinMax(float min, ref float max) { if (min > max) { max = min; } }
            ValidateMinMax(minVelocity, ref maxVelocity);
            ValidateMinMax(minAngularVelocity, ref maxAngularVelocity);
            ValidateMinMax(minScale, ref maxScale);
        }

        public ShipStats ShipStatsPlayer => shipPlayer;
        public ShipStats ShipStatsEnemy => shipEnemy;

        public float MinSpaceRockVelocity => minVelocity;
        public float MaxSpaceRockVelocity => maxVelocity;
        public float MinSpaceRockVelocityAngular => minAngularVelocity;
        public float MaxSpaceRockVelocityAngular => maxAngularVelocity;
        public float MinSpaceRockScale => minScale;
        public float MaxSpaceRockScale => maxScale;
        public float ScaleSpaceRockSpawnRate => scaleSpawnRate;
        public float DistanceSpaceRockSpawn => distanceSpawn;
        public float DistanceSpaceRockMax => distanceMax;
        public float TimeBetweenSpaceRockCleanup => timeBetweenCleanup;
        public float TimeBetweenSpaceRockChance => timeBetweenChance;
        public float ChanceSpaceRockSpawn => chanceSpawn;
        public float ScaleMissileImpactForce => scaleMissileImpactForce;
    }

    [System.Serializable]
    public sealed class ShipStats
    {
        [SerializeField] private Color colorPrimary; // TODO implement colorPrimary?
        [SerializeField] private Color colorSecondary; // TODO implement colorSecondary?
        [SerializeField, Min(0f)] private float multiplierForce;
        [SerializeField, Min(0f)] private float multiplierRotate;
        [SerializeField, Min(0f)] private float velocityMissile;
        [SerializeField, Min(0f)] private float timeMissileLife;
        [SerializeField, Min(0f)] private float timeBetweenShots;

        // public Color ColorPrimary => colorPrimary;
        // public Color ColorSecondary => colorSecondary;
        public float MultiplierForce => multiplierForce;
        public float MultiplierRotate => multiplierRotate;
        public float VelocityMissile => velocityMissile;
        public float TimeMissileLife => timeMissileLife;
        public float TimeBetweenShots => timeBetweenShots;
    }
}
