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
        [SerializeField] private bool destroyMissile;

        [Header("References [SpaceObjectSettings]", order = 99)]
        [SerializeField] private SpaceObject[] prefabSpaceObjects;
        [SerializeField] private ItemDrop[] itemDrops;

        protected virtual void OnValidate()
        {
            GameInfo.ValidateMinMax(minScale, ref maxScale);
            GameInfo.ValidateMinMax(minVelocity, ref maxVelocity);
            GameInfo.ValidateMinMax(minAngularVelocity, ref maxAngularVelocity);
        }

        public float RandomScale => Random.Range(minScale, maxScale);
        public float RandomVelocity => Random.Range(minVelocity, maxVelocity);
        public float RandomAngularVelocity => Random.Range(minAngularVelocity, maxAngularVelocity);
        public SpaceObject RandomSpaceObject => prefabSpaceObjects[Random.Range(0, prefabSpaceObjects.Length)];

        public ItemDrop RandomItemDrop
        {
            get
            {
                int weight = Random.Range(0, TotalWeight);

                foreach (ItemDrop itemDrop in itemDrops)
                {
                    weight -= itemDrop.Weight;

                    if (weight < 0)
                    {
                        return itemDrop;
                    }
                }

                throw new System.Exception("No random item drop selected");
            }
        }

        public int TotalWeight
        {
            get
            {
                int w = 0;
                System.Array.ForEach(itemDrops, drop => w += drop.Weight);
                return w;
            }
        }

        public string Tag => tagName;
        public Color Color => color;
        public float MinScale => minScale;
        public float DistanceMax => distanceMax;
        public bool DestroyMissile => destroyMissile;
    }
}
