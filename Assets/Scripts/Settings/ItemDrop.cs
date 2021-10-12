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

        public ItemInfo Item => this.item;
        public int Weight => this.weight;
        public int RandomAmount => this.minAmount + Random.Range(0, this.maxVariance + 1);
    }
}