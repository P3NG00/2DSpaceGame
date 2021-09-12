using UnityEngine;

namespace SpaceGame.Utilities
{
    public sealed class Util : MonoBehaviour
    {
        public static Vector2 RandomUnitVector
        {
            get
            {
                // TODO cleanup. only expanded for testing output
                Vector2 v = Random.insideUnitCircle;
                print(v.magnitude);
                return v;
            }
        }
        public static float RandomUnit => Random.Range(-1f, 1f);
    }
}
