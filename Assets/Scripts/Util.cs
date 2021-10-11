using UnityEngine;

namespace SpaceGame.Utilities
{
    public static class Util
    {
        public static Vector2 RandomUnitVector => Random.insideUnitCircle.normalized;

        public static void ValidateMinMax(float min, ref float max) { if (min > max) { max = min; } }

        public static T RandomEnum<T>()
        {
            System.Array items = System.Enum.GetValues(typeof(T));
            int index = Random.Range(0, items.Length);
            return (T)items.GetValue(index);
        }
    }
}
