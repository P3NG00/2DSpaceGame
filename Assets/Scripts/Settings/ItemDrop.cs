using UnityEngine;

namespace SpaceGame.Settings
{
    [System.Serializable]
    public sealed class ItemDrop
    {
        [SerializeField] private ItemInfo item;
        [SerializeField] private int weight;
        [SerializeField] private int minAmount;
        [SerializeField] private int maxVariance;

        public ItemInfo ItemInfo => item;
        public int Weight => weight;
        public int RandomAmount => minAmount + Random.Range(0, maxVariance + 1);
    }
}