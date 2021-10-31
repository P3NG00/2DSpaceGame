using UnityEngine;

namespace SpaceGame.Items
{
    [CreateAssetMenu(menuName = "2D Space Game/Item/Info", fileName = "Item Info")]
    public class ItemInfo : ScriptableObject
    {
        [Header("Info [ItemInfo]", order = 0)]
        [SerializeField] private string itemName;
        [SerializeField] private Sprite sprite;
        [SerializeField] private Color color;
        [SerializeField] private int maxStackSize;
        [SerializeField] private bool infinite;
        [SerializeField] private bool loopUsage;
        [SerializeField] private float timeBetweenUses;

        public string ItemName => this.itemName;
        public Sprite Sprite => this.sprite;
        public Color Color => this.color;
        public int MaxStackSize => this.maxStackSize;
        public bool Infinite => this.infinite;
        public bool LoopUsage => this.loopUsage;
        public float TimeBetweenUses => this.timeBetweenUses;
    }
}
