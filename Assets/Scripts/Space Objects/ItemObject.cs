using SpaceGame.Settings;
using UnityEngine;

namespace SpaceGame.SpaceObjects
{
    public sealed class ItemObject : SpaceObject
    {
        [Header("Info (as ItemObject)", order = 10)]
        [SerializeField] private ItemInfo item;
        public int Amount;

        public ItemInfo Item => this.item;

        protected override Color GetColor() => this.item.Color;

        public void SetItemDrop(ItemDrop itemDrop)
        {
            ItemInfo itemInfo = itemDrop.ItemInfo;
            this.item = itemInfo;
            this.SpriteRenderer.sprite = itemInfo.Sprite;
            this.SpriteRenderer.color = itemInfo.Color;
            this.Amount = itemDrop.RandomAmount;
        }
    }
}
