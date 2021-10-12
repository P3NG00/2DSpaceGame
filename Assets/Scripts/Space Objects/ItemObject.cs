using SpaceGame.Items;
using SpaceGame.Settings;
using UnityEngine;

namespace SpaceGame.SpaceObjects
{
    public sealed class ItemObject : SpaceObject
    {
        [Header("Info (as ItemObject)", order = 10)]
        [SerializeField] private ItemStack itemStack;

        public ItemStack ItemStack => this.itemStack;

        protected override Color GetColor() => this.itemStack.Item.Color;

        public void SetItem(ItemStack itemStack)
        {
            this.itemStack = itemStack;
            this.SpriteRenderer.sprite = itemStack.Item.Sprite;
            this.SpriteRenderer.color = itemStack.Item.Color;
        }
    }
}
