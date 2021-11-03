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
        [SerializeField] private float energy = 0f;
        [SerializeField] private float energyMax = 100f;
        [SerializeField] private float energyAmountPerTick = 0.1f;

        [Header("References [ShipPlayer]", order = 99)]
        [SerializeField] private Animator animatorPlayer;

        private float timeCanRegenAfter = 0f;

        // Public Getters
        public float Energy => this.energy;
        public float EnergyRatio => this.Energy / this.energyMax;
        public Animator AnimatorPlayer => this.animatorPlayer;

        public void UpdateStats()
        {
            if (Time.time > this.timeCanRegenAfter)
            {
                this.Heal(this.regenAmountPerTick * Time.deltaTime);
            }

            if (this.energy < this.energyMax)
            {
                this.energy += this.energyAmountPerTick;
            }

            if (this.energy > this.energyMax)
            {
                this.energy = this.energyMax;
            }
        }

        public sealed override UIInventorySlot ItemWeaponSlot() => GameInfo.SlotWeapon;
        public sealed override ItemInfoWeapon GetWeapon() => this.GetItem<ItemInfoWeapon>(GameInfo.SlotWeapon);
        public sealed override ItemInfoDefense GetDefense() => this.GetItem<ItemInfoDefense>(GameInfo.SlotDefense);
        private ItemType GetItem<ItemType>(UIInventorySlot itemSlot) => itemSlot.ItemStack.ItemInfo is ItemType itemType ? itemType : default(ItemType);

        // On damage, update time the player can regen next
        protected override void OnDamage() => this.timeCanRegenAfter = Time.time + this.timeBeforeHealthRegen;

        // protected override void OnDeath() { }
    }
}
