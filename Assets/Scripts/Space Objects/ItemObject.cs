using SpaceGame.Settings;
using UnityEngine;

namespace SpaceGame.SpaceObjects
{
    public sealed class ItemObject : SpaceObject
    {
        [Header("Info (as ItemObject)", order = 10)]
        [SerializeField] private ItemInfo item;
        [SerializeField] private int amount;

        protected override Color GetColor() => item.Color;

        protected override void OnCollided(Collision2D collision)
        {
            // If player triggered and item declared...
            if (collision.gameObject.tag == "Player" & item != null)
            {
                bool given = GameInfo.GiveItem(item, amount);

                if (given)
                {
                    Destroy(gameObject);
                }
            }
        }

        public void SetInfo(ItemInfo item, int amount)
        {
            this.item = item;
            this.amount = amount;
        }
    }
}
