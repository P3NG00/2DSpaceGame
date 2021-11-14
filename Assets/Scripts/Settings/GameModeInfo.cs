using SpaceGame.Recipes;
using SpaceGame.SpaceObjects;
using UnityEngine;

namespace SpaceGame.Settings
{
    [CreateAssetMenu(menuName = "2D Space Game/Game Mode", fileName = "Game Mode Info")]
    public sealed class GameModeInfo : ScriptableObject
    {
        [Header("Info", order = 0)]
        [SerializeField] private float timeBetweenCleanup;
        [SerializeField] private SpaceObjectSpawnableInfo[] spaceObjects;
        [SerializeField] private Recipe[] recipes;

        public float TimeBetweenCleanup => timeBetweenCleanup;
        public SpaceObjectSpawnableInfo[] SpaceObjectsToSpawn => this.spaceObjects;
        public Recipe[] Recipes => this.recipes;
    }
}
