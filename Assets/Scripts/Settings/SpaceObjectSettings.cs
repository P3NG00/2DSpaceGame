using SpaceGame.SpaceObjects;
using UnityEngine;

namespace SpaceGame.Settings
{
    [CreateAssetMenu(menuName = "2D Space Game/Settings/Space Object", fileName = "Space Object Settings")]
    public sealed class SpaceObjectSettings : ScriptableObject
    {
        [Header("Info", order = 20)]
        [SerializeField] private string tagName;
        [SerializeField] private Color color;
        [SerializeField, Min(0f)] private float minScale;
        [SerializeField] private float maxScale;
        [SerializeField, Min(0f)] private float minVelocity;
        [SerializeField] private float maxVelocity;
        [SerializeField, Min(0f)] private float minAngularVelocity;
        [SerializeField] private float maxAngularVelocity;
        [SerializeField, Min(0f)] private float distanceSpawn;
        [SerializeField] private float distanceMax;
        [SerializeField, Min(0)] private int minDropAmount;
        [SerializeField] private int maxDropAmount;
        [SerializeField, Min(0f)] private float widthSpawn;
        [SerializeField] private float scaleSpawnRate;
        [SerializeField, Min(0f)] private float timeBetweenChance;
        [SerializeField, Range(0f, 1f)] private float chanceSpawn;
        [SerializeField, Min(0f)] private float scaleMissileImpactForce;
        [SerializeField, Min(0f)] private float scaleMissileImpactStep;
        [SerializeField] private SpaceObjectSpawnRateType spawnRateType;
        [SerializeField] private SpaceObjectSpawnAreaType spawnAreaType;

        [Header("References", order = 99)]
        [SerializeField] private SpaceObject[] prefabSpaceObjects;
        [SerializeField] private ItemInfo[] itemDrops;

        private void OnValidate()
        {
            void ValidateMinMax(float min, ref float max) { if (min > max) { max = min; } }
            ValidateMinMax(minScale, ref maxScale);
            ValidateMinMax(minVelocity, ref maxVelocity);
            ValidateMinMax(minAngularVelocity, ref maxAngularVelocity);
            ValidateMinMax(distanceSpawn, ref distanceMax);
        }

        public float RandomScale => Random.Range(minScale, maxScale);
        public float RandomVelocity => Random.Range(minVelocity, maxVelocity);
        public float RandomAngularVelocity => Random.Range(minAngularVelocity, maxAngularVelocity);
        public float RandomSpawnDistance => Random.Range(distanceSpawn, distanceMax);
        public float RandomSpawnWidth => Random.Range(-widthSpawn, widthSpawn);
        public int RandomDropAmount => Random.Range(minDropAmount, maxDropAmount + 1);
        public SpaceObject RandomSpaceObject => prefabSpaceObjects[Random.Range(0, prefabSpaceObjects.Length)];
        public ItemInfo RandomItemDrop => itemDrops[Random.Range(0, itemDrops.Length)];

        public string Tag => tagName;
        public Color Color => color;
        public float MinScale => minScale;
        public float DistanceSpawn => distanceSpawn;
        public float DistanceMax => distanceMax;
        public float ScaleSpawnRate => scaleSpawnRate;
        public float TimeBetweenChance => timeBetweenChance;
        public float ChanceSpawn => chanceSpawn;
        public float ScaleMissileImpactForce => scaleMissileImpactForce;
        public float ScaleMissileImpactStep => scaleMissileImpactStep;
        public SpaceObjectSpawnRateType SpawnRateType => spawnRateType;
        public SpaceObjectSpawnAreaType SpawnAreaType => spawnAreaType;
    }

    public enum SpaceObjectSpawnRateType
    {
        Default,
        ScaleWithMagnitude,
        SingleInstance,
    }

    public enum SpaceObjectSpawnAreaType
    {
        Default,
        AroundPlayer,
        FrontOfPlayer,
    }
}
