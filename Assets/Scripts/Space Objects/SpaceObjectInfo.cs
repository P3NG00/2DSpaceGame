using SpaceGame.Items;
using SpaceGame.Utilities;
using UnityEngine;

namespace SpaceGame.SpaceObjects
{
    [CreateAssetMenu(menuName = "2D Space Game/Space Object/Info", fileName = "Space Object Info")]
    public class SpaceObjectInfo : ScriptableObject
    {
        [Header("Info [SpaceObjectSettings]", order = 20)]
        [SerializeField] private string tagName;
        [SerializeField] private Color color;
        [SerializeField] protected float distanceMax;
        [SerializeField] private bool destroyMissile;
        [SerializeField, Min(0f)] private float minSpawnScale;
        [SerializeField] private float maxSpawnScale;
        [SerializeField, Min(0f)] private float destroyBelowScale;
        [SerializeField, Min(0f)] private float minVelocity;
        [SerializeField] private float maxVelocity;
        [SerializeField, Min(0f)] private float minAngularVelocity;
        [SerializeField] private float maxAngularVelocity;
        [SerializeField, Min(0f)] private float damageScaleCollision;
        [SerializeField, Min(0f)] private float damageScaleWeapon;

        [Header("References [SpaceObjectSettings]", order = 99)]
        [SerializeField] private SpaceObject[] prefabSpaceObjects;
        [SerializeField] private ItemDrop[] itemDrops;

        protected virtual void OnValidate()
        {
            Util.ValidateMinMax(this.minSpawnScale, ref this.maxSpawnScale);
            Util.ValidateMinMax(this.destroyBelowScale, ref this.minSpawnScale);
            Util.ValidateMinMax(this.minVelocity, ref this.maxVelocity);
            Util.ValidateMinMax(this.minAngularVelocity, ref this.maxAngularVelocity);
        }

        public float RandomScale => Random.Range(this.minSpawnScale, this.maxSpawnScale);
        public float RandomVelocity => Random.Range(this.minVelocity, this.maxVelocity);
        public float RandomAngularVelocity => Random.Range(this.minAngularVelocity, this.maxAngularVelocity);
        public SpaceObject RandomSpaceObject => this.prefabSpaceObjects[Random.Range(0, this.prefabSpaceObjects.Length)];

        public ItemStack RandomItemDrop
        {
            get
            {
                int totalWeight = 0;
                System.Array.ForEach(itemDrops, drop => totalWeight += drop.Weight);
                int weight = Random.Range(0, totalWeight);

                foreach (ItemDrop drop in this.itemDrops)
                {
                    weight -= drop.Weight;

                    if (weight < 0)
                    {
                        return new ItemStack(drop.Item, drop.RandomAmount);
                    }
                }

                // This should not be reached, something should return in the loop
                throw new System.Exception("No random item drop selected");
            }
        }

        public string Tag => this.tagName;
        public Color Color => this.color;
        public float DestroyBelowScale => this.destroyBelowScale;
        public float DistanceMax => this.distanceMax;
        public float DamageScaleCollision => this.damageScaleCollision;
        public float DamageScaleProjectile => this.damageScaleWeapon;
        public bool DestroyMissile => this.destroyMissile;
    }
}
