using UnityEngine;

namespace SpaceGame.Settings
{
    [CreateAssetMenu(menuName = "2D Space Game/Item/Item", fileName = "Item")]
    public class Item : ScriptableObject
    {
        [Header("Info", order = 0)]
        [SerializeField] private string itemName;
        [SerializeField] private Sprite sprite;
        [SerializeField] private Color color;
        [SerializeField] private int maxStackSize;

        public string ItemName => this.itemName;
        public Sprite Sprite => this.sprite;
        public Color Color => this.color;
        public int MaxStackSize => this.maxStackSize;

        public virtual void Use() { }
    }
}
