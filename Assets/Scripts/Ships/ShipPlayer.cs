using SpaceGame.Items;
using SpaceGame.UI;
using UnityEngine;

namespace SpaceGame.Ships
{
    public sealed class ShipPlayer : Ship
    {
        [Header("Info [ShipPlayer]", order = 5)]
        [SerializeField] private float timeBeforeHealthRegen = 3f;
        [SerializeField] private float regenAmountPerTick = 0.1f;

        [Header("References [ShipPlayer]", order = 99)]
        [SerializeField] private Animator animatorPlayer;

        private float timeCanRegenAfter = 0f;

        // Public Getters
        public Animator Animator => this.animatorPlayer;

        public void UpdateHealth()
        {
            if (Time.time > this.timeCanRegenAfter)
            {
                this.Heal(this.regenAmountPerTick);
            }
        }

        public sealed override UIInventorySlot ItemWeaponSlot() => GameInfo.SlotWeapon;
        public sealed override ItemInfo GetItemInfo() => GameInfo.SlotWeapon.ItemStack.ItemInfo;

        // On damage, update time the player can regen next
        protected override void OnDamage() => this.timeCanRegenAfter = Time.time + this.timeBeforeHealthRegen;

        // protected override void OnDeath() { }
    }
}
