using UnityEngine;

namespace SpaceGame.Utilities
{
    public static class Util
    {
        public static void ToggleBool(ref bool b) => b = !b;
        public static void ToggleActive(GameObject obj) => obj.SetActive(!obj.activeSelf);
        public static void ValidateMinMax(float min, ref float max) { if (min > max) { max = min; } }

        public static Vector2 RandomUnitVector => Random.insideUnitCircle.normalized;

        public static T RandomEnum<T>()
        {
            System.Array enums = System.Enum.GetValues(typeof(T));
            return (T)enums.GetValue(Random.Range(0, enums.Length));
        }
    }
}
