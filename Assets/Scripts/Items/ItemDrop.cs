using UnityEngine;

namespace SpaceGame.Settings
{
    [System.Serializable]
    public sealed class ItemDrop
    {
        [SerializeField] private Item item;
        [SerializeField] private int weight;
        [SerializeField] private int minAmount;
        [SerializeField] private int maxVariance;

        public Item Item => this.item;
        public int Weight => this.weight;
        public int RandomAmount => this.minAmount + Random.Range(0, this.maxVariance + 1);
    }
}
