using SpaceGame.SpaceObjects;
using UnityEngine;

namespace SpaceGame.Settings
{
    [CreateAssetMenu(menuName = "2D Space Game/Space Object", fileName = "Space Object Settings")]
    public class SpaceObjectSettings : ScriptableObject
    {
        [Header("Info [SpaceObjectSettings]", order = 20)]
        [SerializeField] private string tagName;
        [SerializeField] private Color color;
        [SerializeField, Min(0f)] private float minScale;
        [SerializeField] private float maxScale;
        [SerializeField, Min(0f)] private float minVelocity;
        [SerializeField] private float maxVelocity;
        [SerializeField, Min(0f)] private float minAngularVelocity;
        [SerializeField] private float maxAngularVelocity;
        [SerializeField] protected float distanceMax;

        [Header("References [SpaceObjectSettings]", order = 99)]
        [SerializeField] private SpaceObject[] prefabSpaceObjects;
        [SerializeField] private ItemDrop[] itemDrops;

        private int totalWeight = 0;

        protected void ValidateMinMax(float min, ref float max)
        {
            if (min > max)
            {
                max = min;
            }
        }

        protected virtual void OnValidate()
        {
            ValidateMinMax(minScale, ref maxScale);
            ValidateMinMax(minVelocity, ref maxVelocity);
            ValidateMinMax(minAngularVelocity, ref maxAngularVelocity);
        }

        private void Awake()
        {
            foreach (ItemDrop drop in itemDrops)
            {
                totalWeight += drop.Weight;
            }
        }

        public float RandomScale => Random.Range(minScale, maxScale);
        public float RandomVelocity => Random.Range(minVelocity, maxVelocity);
        public float RandomAngularVelocity => Random.Range(minAngularVelocity, maxAngularVelocity);
        public SpaceObject RandomSpaceObject => prefabSpaceObjects[Random.Range(0, prefabSpaceObjects.Length)];
        public ItemDrop RandomItemDrop
        {
            get
            {
                int randomWeight = Random.Range(0, totalWeight);
                int weight;

                foreach (ItemDrop itemDrop in itemDrops)
                {
                    weight = itemDrop.Weight;

                    if (randomWeight < weight)
                    {
                        return itemDrop;
                    }

                    randomWeight -= weight;
                }

                return null;
            }
        }

        public string Tag => tagName;
        public Color Color => color;
        public float MinScale => minScale;
        public float DistanceMax => distanceMax;
    }
}
