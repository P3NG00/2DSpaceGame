using UnityEngine;
using SpaceGame.Effects;

namespace SpaceGame.Items
{
    public abstract class ItemInfoEffect : ItemInfo
    {
        [Header("Info [ItemInfoEffect]", order = 2)]
        [SerializeField] private EffectList effects;

        public EffectList Effects => this.effects;

        protected virtual void OnEnable() => this.effects.InitializeList();
    }
}
