using SpaceGame.Settings;
using UnityEngine;

namespace SpaceGame.SpaceObjects
{
    public sealed class ItemObject : SpaceObject
    {
        [Header("Info (as ItemObject)", order = 10)]
        public ItemInfo Item;
        public int Amount;

        protected override Color GetColor() => Item.Color;

        public void SetInfo(ItemInfo item, int amount)
        {
            this.Item = item;
            this.Amount = amount;

            SpriteRenderer.sprite = item.Sprite;
            SpriteRenderer.color = item.Color;
        }
    }
}
