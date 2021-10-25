using SpaceGame.SpaceObjects;
using UnityEngine;

namespace SpaceGame.Projectiles
{
    [CreateAssetMenu(menuName = "2D Space Game/Projectile/Info", fileName = "Projectile Info")]
    public sealed class ProjectileInfo : ScriptableObject
    {
        [Header("Info", order = 0)]
        [SerializeField] private string projectileName;
        [SerializeField] private float damage;
        [SerializeField] private float damageMultShip;
        [SerializeField] private float damageMultObject;
        [SerializeField] private float magnitude;

        [Header("References", order = 100)]
        [SerializeField] private SpaceObjectProjectile projectileObject;
        // TODO implement projectileObject for spawning projectile prefab and setting variables

        public string Name => this.projectileName;
        public float DamageShip => this.damage * this.damageMultShip;
        public float DamageObject => this.damage * this.damageMultObject;
        public float Magnitude => this.magnitude;
        public SpaceObjectProjectile ProjectileObject => this.projectileObject;
    }
}
