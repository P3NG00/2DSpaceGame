using SpaceGame.Items;
using UnityEngine;

namespace SpaceGame.Projectiles
{
    [CreateAssetMenu(menuName = "2D Space Game/Projectile/Info", fileName = "Projectile Info")]
    public sealed class ProjectileInfo : ScriptableObject
    {
        [Header("Info", order = 0)]
        [SerializeField] private string projectileName;
        [SerializeField] private float damageShip;
        [SerializeField] private float damageSpaceObject;
        [SerializeField] private float magnitude;
        [SerializeField] private float lifetime;

        [Header("References", order = 100)]
        [SerializeField] private ItemInfoProjectile correspondingItem;
        [SerializeField] private Projectile projectileObject;

        public string Name => this.projectileName;
        public float DamageShip => this.damageShip;
        public float DamageSpaceObject => this.damageSpaceObject;
        public float Magnitude => this.magnitude;
        public float Lifetime => this.lifetime;
        public ItemInfoProjectile Item => this.correspondingItem;
        public Projectile ProjectileObject => this.projectileObject;
    }
}
