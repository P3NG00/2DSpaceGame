using SpaceGame.Settings;
using UnityEngine;

namespace SpaceGame.SpaceObjects
{
    public sealed class ItemObject : SpaceObject
    {
        [Header("Info (as ItemObject)", order = 10)]
        [SerializeField] private ItemInfo item;
        [SerializeField] private int amount;

        public ItemInfo Item => item;
        public int Amount => amount;

        protected override Color GetColor() => item.Color;

        public void SetInfo(ItemInfo item, int amount)
        {
            this.item = item;
            this.amount = amount;

            SpriteRenderer.sprite = item.Sprite;
            SpriteRenderer.color = item.Color;
        }
    }
}
