using UnityEngine;

namespace SpaceGame.Effects
{
    [System.Serializable]
    sealed class EffectStat
    {
        [SerializeField] private Enums.Effect effect;
        [SerializeField] private float time;

        public Enums.Effect Effect => this.effect;
        public float Time => this.time;
    }
}
