using SpaceGame.Utilities;
using UnityEngine;

namespace SpaceGame.Settings
{
    [CreateAssetMenu(menuName = "2D Space Game/Space Object/Spawnable", fileName = "Space Object Spawnable Settings")]
    public sealed class SpaceObjectSpawnableSettings : SpaceObjectSettings
    {
        [Header("Info [SpaceObjectSpawnableSettings]", order = 30)]
        [SerializeField, Min(0f)] private float distanceSpawn;
        [SerializeField, Min(0f)] private float widthSpawn;
        [SerializeField] private float scaleSpawnRate;
        [SerializeField, Min(0f)] private float timeBetweenChance;
        [SerializeField, Range(0f, 1f)] private float chanceSpawn;
        [SerializeField, Min(0f)] private float scaleMissileDamage;
        [SerializeField, Min(0f)] private float scaleMissileImpactForce;
        [SerializeField] private Enums.SpaceObjectSpawnRateType spawnRateType;
        [SerializeField] private Enums.SpaceObjectSpawnAreaType spawnAreaType;

        [Header("DEBUG [SpaceObjectSpawnableSettings]", order = 101)]
        [SerializeField] private bool debugAnnounceSpawn;

        protected override void OnValidate()
        {
            base.OnValidate();
            Util.ValidateMinMax(this.distanceSpawn, ref this.distanceMax);
        }

        public float RandomSpawnDistance => Random.Range(this.distanceSpawn, this.distanceMax);
        public float RandomSpawnWidth => Random.Range(-this.widthSpawn, this.widthSpawn);

        public float ScaleSpawnRate => this.scaleSpawnRate;
        public float TimeBetweenChance => this.timeBetweenChance;
        public float ChanceSpawn => this.chanceSpawn;
        public float ScaleMissileDamage => this.scaleMissileDamage;
        public float ScaleMissileImpactForce => this.scaleMissileImpactForce;
        public Enums.SpaceObjectSpawnRateType SpawnRateType => this.spawnRateType;
        public Enums.SpaceObjectSpawnAreaType SpawnAreaType => this.spawnAreaType;

        public bool DebugAnnounceSpawn => this.debugAnnounceSpawn;
    }
}
