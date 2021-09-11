using SpaceGame.SpaceObjects;
using UnityEngine;

namespace SpaceGame.Settings
{
    [CreateAssetMenu(menuName = "2D Space Game/Settings/Space Object", fileName = "Space Object Settings")]
    public sealed class SpaceObjectSettings : ScriptableObject
    {
        [Header("Info", order = 20)]
        [SerializeField, Min(0f)] private float minScale;
        [SerializeField] private float maxScale;
        [SerializeField, Min(0f)] private float minVelocity;
        [SerializeField] private float maxVelocity;
        [SerializeField, Min(0f)] private float minAngularVelocity;
        [SerializeField] private float maxAngularVelocity;
        [SerializeField] private float scaleSpawnRate;
        [SerializeField] private float distanceSpawn;
        [SerializeField] private float distanceMax;
        [SerializeField] private float timeBetweenCleanup;
        [SerializeField, Min(0f)] private float timeBetweenChance;
        [SerializeField, Range(0f, 1f)] private float chanceSpawn;
        [SerializeField, Min(0f)] private float scaleMissileImpactForce;

        [Header("References", order = 99)]
        [SerializeField] private SpaceObject prefabSpaceObject;

        private void ValidateMinMax(float min, ref float max)
        {
            if (min > max)
            {
                max = min;
            }
        }

        private void OnValidate()
        {
            ValidateMinMax(minScale, ref maxScale);
            ValidateMinMax(minVelocity, ref maxVelocity);
            ValidateMinMax(minAngularVelocity, ref maxAngularVelocity);
        }

        public float MinVelocity => minVelocity;
        public float MaxVelocity => maxVelocity;
        public float MinVelocityAngular => minAngularVelocity;
        public float MaxVelocityAngular => maxAngularVelocity;
        public float MinScale => minScale;
        public float MaxScale => maxScale;
        public float ScaleSpawnRate => scaleSpawnRate;
        public float DistanceSpawn => distanceSpawn;
        public float DistanceMax => distanceMax;
        public float TimeBetweenCleanup => timeBetweenCleanup;
        public float TimeBetweenChance => timeBetweenChance;
        public float ChanceSpawn => chanceSpawn;
        public float ScaleMissileImpactForce => scaleMissileImpactForce;

        public SpaceObject Prefab => prefabSpaceObject;
    }
}
