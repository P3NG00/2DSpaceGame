using SpaceGame.Settings;
using UnityEngine;

namespace SpaceGame.SpaceObjects
{
    public sealed class ItemObject : SpaceObject
    {
        [Header("Info (as ItemObject)", order = 10)]
        [SerializeField] private ItemInfo item;
        public int Amount;

        public ItemInfo Item
        {
            get => item;
            set
            {
                this.item = value;
                this.SpriteRenderer.sprite = this.item.Sprite;
                this.SpriteRenderer.color = this.item.Color;
            }
        }

        protected override Color GetColor() => this.Item.Color;
    }
}
