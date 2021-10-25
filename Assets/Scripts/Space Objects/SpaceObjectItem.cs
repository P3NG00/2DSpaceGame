using SpaceGame.Items;
using UnityEngine;

namespace SpaceGame.SpaceObjects
{
    public sealed class SpaceObjectItem : SpaceObject
    {
        [Header("Info (as ItemObject)", order = 10)]
        [SerializeField] private ItemStack itemStack;

        public ItemStack ItemStack => this.itemStack;

        protected override Color GetColor() => this.itemStack.ItemInfo.Color;

        public void SetItem(ItemStack itemStack)
        {
            this.itemStack = itemStack;
            this.SpriteRenderer.sprite = itemStack.ItemInfo.Sprite;
            this.SpriteRenderer.color = itemStack.ItemInfo.Color;
        }
    }
}
