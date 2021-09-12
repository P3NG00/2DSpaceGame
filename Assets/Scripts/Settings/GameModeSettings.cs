using UnityEngine;

namespace SpaceGame.Settings
{
    [CreateAssetMenu(menuName = "2D Space Game/Settings/Game Mode", fileName = "GameMode Settings")]
    public sealed class GameModeSettings : ScriptableObject
    {
        [Header("Info", order = 0)]
        [SerializeField] private float timeBetweenCleanup;
        [SerializeField] private SpaceObjectSettings[] spaceObjects;

        public float TimeBetweenCleanup => timeBetweenCleanup;
        public SpaceObjectSettings[] SpaceObjectsToSpawn => spaceObjects;
    }
}
