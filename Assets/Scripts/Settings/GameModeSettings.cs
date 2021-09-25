using UnityEngine;

namespace SpaceGame.Settings
{
    [CreateAssetMenu(menuName = "2D Space Game/Game Mode", fileName = "GameMode Settings")]
    public sealed class GameModeSettings : ScriptableObject
    {
        [Header("Info", order = 0)]
        [SerializeField] private float timeBetweenCleanup;
        [SerializeField] private SpaceObjectSpawnableSettings[] spaceObjects;

        public float TimeBetweenCleanup => timeBetweenCleanup;
        public SpaceObjectSpawnableSettings[] SpaceObjectsToSpawn => spaceObjects;
    }
}
