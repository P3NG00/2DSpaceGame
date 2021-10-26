using SpaceGame.Ships;
using UnityEngine;

namespace SpaceGame.Items
{
    [CreateAssetMenu(menuName = "2D Space Game/Item/Item", fileName = "Item")]
    public class ItemInfo : ScriptableObject
    {
        [Header("Info", order = 0)]
        [SerializeField] private string itemName;
        [SerializeField] private Sprite sprite;
        [SerializeField] private Color color;
        [SerializeField] private int maxStackSize;
        [SerializeField] private bool infinite;

        public string ItemName => this.itemName;
        public Sprite Sprite => this.sprite;
        public Color Color => this.color;
        public int MaxStackSize => this.maxStackSize;
        public bool Infinite => this.infinite;

        public virtual void Use(Ship source) { }
    }
}
