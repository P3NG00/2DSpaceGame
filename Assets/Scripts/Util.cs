using UnityEngine;

namespace SpaceGame.Utilities
{
    public sealed class Util : MonoBehaviour
    {
        public static Vector2 RandomUnitVector => new Vector2(RandomUnit, RandomUnit).normalized;
        public static float RandomUnit => Random.Range(-1f, 1f);
    }
}
