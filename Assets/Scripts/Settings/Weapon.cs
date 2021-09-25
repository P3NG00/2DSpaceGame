using UnityEngine;

namespace SpaceGame.Settings
{
    [CreateAssetMenu(menuName = "2D Space Game/Weapon", fileName = "Weapon Settings")]
    public sealed class Weapon : ScriptableObject
    {
        [Header("Info [Weapon]", order = 0)]
        [SerializeField] private float projectileSpeed;
        [SerializeField] private float lifetimeMax;
        [SerializeField] private float timeBetweenShots;

        public float ProjectileSpeed => projectileSpeed;
        public float LifetimeMax => lifetimeMax;
        public float TimeBetweenShots => timeBetweenShots;
    }
}