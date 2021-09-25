using SpaceGame.Settings;
using UnityEngine;

namespace SpaceGame.Ships
{
    public abstract class Ship : MonoBehaviour
    {
        [Header("Info", order = 0)]
        [SerializeField] private ShipStats stats;

        [Header("References (as Ship)", order = 90)]
        [SerializeField] private new Rigidbody2D rigidbody;
        [SerializeField] private SpriteRenderer srPrimary;
        [SerializeField] private SpriteRenderer srSecondary;

        [Header("DEBUG", order = 100)]
        [SerializeField] private bool FORCE_VALIDATE;

        public ShipStats Stats => stats;
        public Rigidbody2D Rigidbody => rigidbody;

        private void OnValidate()
        {
            srPrimary.color = stats.ColorPrimary;
            srSecondary.color = stats.ColorSecondary;

            FORCE_VALIDATE = false;
        }

        public void AddForce()
        {
            Vector2 velocity = transform.up * stats.MultiplierForce;
            rigidbody.AddForce(velocity);
        }

        public void Rotate(float rotation)
        {
            Vector2 force = transform.right * rotation;
            force *= stats.MultiplierRotate * Time.deltaTime;

            Vector2 position = transform.position;
            position += (Vector2)transform.up * 0.5773f; // TODO test increasing height?

            rigidbody.AddForceAtPosition(force, position);
        }

        public void Fire()
        {
            Vector3 posMissile = transform.position;
            posMissile += transform.up * 0.1f;

            Rigidbody2D missile = Instantiate(GameInfo.PrefabMissile, posMissile, transform.rotation);
            missile.velocity = transform.up * stats.Weapon.ProjectileSpeed;

            Destroy(missile.gameObject, stats.Weapon.LifetimeMax);
        }
    }
}
