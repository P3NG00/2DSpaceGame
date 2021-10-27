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

        // i have no idea if this actually works, it was concept in my head and i was gonna use it
        // but i decided to just use a list instead
        public static T[] MergeArrays<T>(params T[][] arrays)
        {
            int totalLength = 0, index = 0;
            System.Array.ForEach(arrays, array => totalLength += array.Length);
            T[] mergedArray = new T[totalLength];
            System.Array.ForEach(arrays, array => System.Array.ForEach(array, a => mergedArray[index++] = a));
            return mergedArray;
        }
    }
}
