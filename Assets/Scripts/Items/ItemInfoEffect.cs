using UnityEngine;
using SpaceGame.Effects;

namespace SpaceGame.Items
{
    public abstract class ItemInfoEffect : ItemInfo
    {
        [Header("Info [ItemInfoEffect]", order = 2)]
        [SerializeField] private EffectList effectAttack;

        public EffectList Effects => this.effectAttack;

        protected virtual void Awake() => this.effectAttack.InitializeList();
    }
}
