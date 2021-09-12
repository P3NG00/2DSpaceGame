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
        [SerializeField, Min(0f)] private float distanceSpawn;
        [SerializeField] private float distanceMax;
        [SerializeField] private float scaleSpawnRate;
        [SerializeField] private float timeBetweenCleanup;
        [SerializeField, Min(0f)] private float timeBetweenChance;
        [SerializeField, Range(0f, 1f)] private float chanceSpawn;
        [SerializeField, Min(0f)] private float scaleMissileImpactForce;

        [Header("References", order = 99)]
        [SerializeField] private SpaceObject[] prefabSpaceObjects;

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
            ValidateMinMax(distanceSpawn, ref distanceMax);
        }

        public float RandomScale => Random.Range(minScale, maxScale);
        public float RandomVelocity => Random.Range(minVelocity, maxVelocity);
        public float RandomAngularVelocity => Random.Range(minAngularVelocity, maxAngularVelocity);
        public float RandomSpawnDistance => Random.Range(distanceSpawn, distanceMax);

        public float MinScale => minScale;
        public float ScaleSpawnRate => scaleSpawnRate;
        public float TimeBetweenCleanup => timeBetweenCleanup;
        public float TimeBetweenChance => timeBetweenChance;
        public float ChanceSpawn => chanceSpawn;
        public float ScaleMissileImpactForce => scaleMissileImpactForce;
        public SpaceObject RandomSpaceObject => prefabSpaceObjects[Random.Range(0, prefabSpaceObjects.Length)];
    }
}
