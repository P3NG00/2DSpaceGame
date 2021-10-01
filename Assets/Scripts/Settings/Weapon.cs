using UnityEngine;

namespace SpaceGame.Settings
{
    [CreateAssetMenu(menuName = "2D Space Game/Weapon", fileName = "Weapon Settings")]
    public sealed class Weapon : ScriptableObject
    {
        [Header("Info [Weapon]", order = 0)]
        [SerializeField, Min(0f)] private float damage;
        [SerializeField, Min(0f)] private float projectileSpeed;
        [SerializeField, Min(0f)] private float lifetimeMax = 1f;
        [SerializeField, Min(0f)] private float timeBetweenShots;
        [SerializeField, Min(1)] private int amountOfShots = 1;
        [SerializeField, Min(0f)] private float angleBetweenShots;

        public float Damage => damage;
        public float ProjectileSpeed => projectileSpeed;
        public float LifetimeMax => lifetimeMax;
        public float TimeBetweenShots => timeBetweenShots;
        public int AmountOfShots => amountOfShots;
        public float AngleBetweenShots => angleBetweenShots;
    }
}