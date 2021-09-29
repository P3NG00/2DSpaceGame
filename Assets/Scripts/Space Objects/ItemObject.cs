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
                SpriteRenderer.sprite = item.Sprite;
                SpriteRenderer.color = item.Color;
            }
        }

        protected override Color GetColor() => Item.Color;
    }
}
