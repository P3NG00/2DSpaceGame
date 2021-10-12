using SpaceGame.Items;
using SpaceGame.SpaceObjects;
using SpaceGame.Utilities;
using UnityEngine;

namespace SpaceGame.Settings
{
    [CreateAssetMenu(menuName = "2D Space Game/Space Object/Space Object", fileName = "Space Object Settings")]
    public class SpaceObjectSettings : ScriptableObject
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

        [Header("References [SpaceObjectSettings]", order = 99)]
        [SerializeField] private SpaceObject[] prefabSpaceObjects;
        [SerializeField] private ItemDrop[] itemDrops;

        private int totalWeight = 0;

        protected virtual void OnValidate()
        {
            Util.ValidateMinMax(this.minSpawnScale, ref this.maxSpawnScale);
            Util.ValidateMinMax(this.destroyBelowScale, ref this.minSpawnScale);
            Util.ValidateMinMax(this.minVelocity, ref this.maxVelocity);
            Util.ValidateMinMax(this.minAngularVelocity, ref this.maxAngularVelocity);
        }

        private void Awake()
        {
            System.Array.ForEach(itemDrops, drop => totalWeight += drop.Weight);
        }

        public float RandomScale => Random.Range(this.minSpawnScale, this.maxSpawnScale);
        public float RandomVelocity => Random.Range(this.minVelocity, this.maxVelocity);
        public float RandomAngularVelocity => Random.Range(this.minAngularVelocity, this.maxAngularVelocity);
        public SpaceObject RandomSpaceObject => this.prefabSpaceObjects[Random.Range(0, this.prefabSpaceObjects.Length)];

        public ItemStack RandomItemDrop
        {
            get
            {
                int weight = Random.Range(0, this.totalWeight);

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
        public bool DestroyMissile => this.destroyMissile;
    }
}
