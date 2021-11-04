using System;
using SpaceGame.Utilities;
using UnityEngine;

namespace SpaceGame.Effects
{
    [System.Serializable]
    public sealed class EffectList
    {
        // This variable should only be used for Unity Editor,
        // these values should be added into this.effectTime upon start
        [SerializeField] private EffectStat[] effects = new EffectStat[0];

        private float[] effectTime;

        public void InitializeList()
        {
            Enums.Effect[] effectEnums = Util.GetEnums<Enums.Effect>();
            this.effectTime = new float[effectEnums.Length];

            for (int i = 0; i < effectEnums.Length; i++)
            {
                this.effectTime[i] = 0f;
            }

            EffectStat effectStat;

            for (int i = 0; i < this.effects.Length; i++)
            {
                effectStat = this.effects[i];
                int index = (int)effectStat.Effect;
                this.effectTime[index] += effectStat.Time;
            }

            System.Array.ForEach(this.effects, effect => this.AddEffectTime(effect.Effect, effect.Time));
        }

        // public float GetEffectTime(Enums.Effect effect) => this.effectTime[(int)effect];
        // public void AddEffectTime(Enums.Effect effect, float time) => this.effectTime[(int)effect] += time;
        // public bool HasEffect(Enums.Effect effect) => this.GetEffectTime(effect) > 0f;

        public float GetEffectTime(Enums.Effect effect) => this.effectTime[(int)effect];
        public bool HasEffect(Enums.Effect effect) => this.GetEffectTime(effect) > 0f;

        public void AddEffectTime(Enums.Effect effect, float time)
        {
            int index = (int)effect;
            float newTime = this.effectTime[index] + time;

            if (newTime < 0f)
            {
                newTime = 0f;
            }

            this.effectTime[index] = newTime;
        }

        public void RemoveTimeFromAll(float time)
        {
            for (int i = 0; i < this.effectTime.Length; i++)
            {
                if (this.effectTime[i] > 0f)
                {
                    this.effectTime[i] -= time;

                    if (this.effectTime[i] < 0f)
                    {
                        this.effectTime[i] = 0f;
                    }
                }
            }
        }

        public static EffectList operator +(EffectList list0, EffectList list1) => EffectList.Operate(list0, list1, (f0, f1) => f0 + f1);
        public static EffectList operator -(EffectList list0, EffectList list1) => EffectList.Operate(list0, list1, (f0, f1) => f0 - f1);

        private static EffectList Operate(EffectList list0, EffectList list1, Func<float, float, float> func)
        {
            EffectList effectList = new EffectList();
            effectList.InitializeList();
            Enums.Effect effect;

            try
            {
                for (int i = 0; i < list0.effectTime.Length; i++)
                {
                    effect = (Enums.Effect)i;
                    effectList.AddEffectTime(effect, func.Invoke(list0.GetEffectTime(effect), list1.GetEffectTime(effect)));
                }
            }
            catch (System.Exception e)
            {
                Debug.LogError(e.Message);
            }

            return effectList;
        }
    }
}
