using UnityEngine;

namespace SpaceGame.Settings
{
    [CreateAssetMenu(menuName = "2D Space Game/Weapon", fileName = "Weapon Settings")]
    public sealed class Weapon : ScriptableObject
    {
        [Header("Info [Weapon]", order = 0)]
        [SerializeField, Min(0f)] private float multShip;
        [SerializeField, Min(0f)] private float multSpaceRock;
        [SerializeField, Min(0f)] private float projectileSpeed;
        [SerializeField, Min(0f)] private float lifetimeMax = 1f;
        [SerializeField, Min(0f)] private float timeBetweenShots;
        [SerializeField, Min(1)] private int amountOfShots = 1;
        [SerializeField, Min(0f)] private float angleBetweenShots;

        public float MultShip => this.multShip;
        public float MultSpaceRock => this.multSpaceRock;
        public float ProjectileSpeed => this.projectileSpeed;
        public float LifetimeMax => this.lifetimeMax;
        public float TimeBetweenShots => this.timeBetweenShots;
        public int AmountOfShots => this.amountOfShots;
        public float AngleBetweenShots => this.angleBetweenShots;
    }
}