using SpaceGame.Ships;
using UnityEngine;

namespace SpaceGame.Projectiles
{
    public sealed class Projectile : MonoBehaviour
    {
        [Header("References [Projectile]", order = 95)]
        [SerializeField] private new Rigidbody2D rigidbody;
        [SerializeField] private SpriteRenderer spriteRenderer;

        [Header("DEBUG", order = 100)]
        public ProjectileInfo ProjectileInfo;
        public Ship SourceShip;

        public Rigidbody2D Rigidbody => this.rigidbody;

        private void OnTriggerEnter2D(Collider2D collider)
        {
            // TODO move all projectile collisions (from things like space objects or ships) into here
        }
    }
}