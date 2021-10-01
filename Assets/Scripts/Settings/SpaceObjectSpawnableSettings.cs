using UnityEngine;

namespace SpaceGame.Settings
{
    [CreateAssetMenu(menuName = "2D Space Game/Space Object Spawnable", fileName = "Space Object Spawnable Settings")]
    public sealed class SpaceObjectSpawnableSettings : SpaceObjectSettings
    {
        [Header("Info [SpaceObjectSpawnableSettings]", order = 30)]
        [SerializeField, Min(0f)] private float distanceSpawn;
        [SerializeField, Min(0f)] private float widthSpawn;
        [SerializeField] private float scaleSpawnRate;
        [SerializeField, Min(0f)] private float timeBetweenChance;
        [SerializeField, Range(0f, 1f)] private float chanceSpawn;
        [SerializeField, Min(0f)] private float scaleMissileImpactForce; // TODO replace with scale for how quickly incoming damage from missiles affects the step
        [SerializeField, Min(0f)] private float scaleMissileImpactStep; // TODO remove
        [SerializeField] private SpaceObjectSpawnRateType spawnRateType;
        [SerializeField] private SpaceObjectSpawnAreaType spawnAreaType;

        protected override void OnValidate()
        {
            base.OnValidate();
            GameInfo.ValidateMinMax(distanceSpawn, ref distanceMax);
        }

        public float RandomSpawnDistance => Random.Range(distanceSpawn, distanceMax);
        public float RandomSpawnWidth => Random.Range(-widthSpawn, widthSpawn);

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
