using SpaceGame.Settings;
using UnityEngine;

namespace SpaceGame.Ships
{
    public abstract class Ship : MonoBehaviour
    {
        [Header("Info", order = 0)]
        [SerializeField] private float health;
        [SerializeField] private float maxHealth;
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
            GameInfo.ValidateMinMax(health, ref maxHealth);

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
            float torque = -rotation * stats.MultiplierRotate * Time.deltaTime;
            rigidbody.AddTorque(torque);
        }

        public void Fire()
        {
            Vector3 posMissile = transform.position;
            posMissile += transform.up * 0.1f;

            Rigidbody2D missile = Instantiate(GameInfo.PrefabMissile, posMissile, transform.rotation);
            missile.velocity = transform.up * stats.Weapon.ProjectileSpeed;

            Destroy(missile.gameObject, stats.Weapon.LifetimeMax);
        }

        public void ApplyDrag(bool drag)
        {
            rigidbody.drag = drag ? Stats.Drag : 0;
        }

        public void Heal(float amount)
        {
            health += amount;

            if (health > maxHealth)
            {
                health = maxHealth;
            }
        }

        public void Damage(float damage)
        {
            health -= damage;

            if (health <= 0f)
            {
                health = 0f;
                Destroy(gameObject);
                OnDeath();
            }
        }

        protected virtual void OnDeath() { }
    }
}
