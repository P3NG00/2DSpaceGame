using UnityEngine;

namespace SpaceGame.Utilities
{
    public sealed class Util : MonoBehaviour
    {
        public static Vector2 RandomUnitVector => Random.insideUnitCircle.normalized;
    }
}
