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
        [SerializeField] private Rigidbody2D prefabMissile;

        [Header("DEBUG", order = 100)]
        [SerializeField] private bool FORCE_VALIDATE;

        public Rigidbody2D Rigidbody => rigidbody;
        public ShipStats Stats => stats;

        private void OnValidate()
        {
            if (FORCE_VALIDATE)
            {
                srPrimary.color = stats.ColorPrimary;
                srSecondary.color = stats.ColorSecondary;

                FORCE_VALIDATE = false;
            }
        }

        protected void AddForce()
        {
            Vector2 direction = transform.up;
            Vector2 velocity = direction * stats.MultiplierForce;
            rigidbody.AddForce(velocity);
        }

        protected void Rotate(float rotation)
        {
            Vector2 force = transform.right * rotation;
            force *= stats.MultiplierRotate * Time.deltaTime;

            Vector2 position = transform.position;
            position += (Vector2)transform.up * 0.5773f;

            rigidbody.AddForceAtPosition(force, position);
        }

        protected void Fire()
        {
            Vector3 posMissile = transform.position;
            posMissile += transform.up * 0.1f;

            Rigidbody2D missile = Instantiate(prefabMissile, posMissile, transform.rotation);

            missile.velocity = transform.up * stats.VelocityMissile;

            Destroy(missile.gameObject, stats.TimeMissileLife);
        }
    }
}
