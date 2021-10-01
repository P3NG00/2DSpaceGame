using System.Collections.Generic;
using SpaceGame.Settings;
using SpaceGame.SpaceObjects;
using UnityEngine;

namespace SpaceGame.Ships
{
    public abstract class Ship : MonoBehaviour
    {
        [Header("Info", order = 0)]
        [SerializeField] private float health;
        [SerializeField] private float maxHealth;
        [SerializeField] private ShipStats stats;
        [SerializeField] private Weapon weapon;

        [Header("References (as Ship)", order = 90)]
        [SerializeField] private new Rigidbody2D rigidbody;
        [SerializeField] private SpriteRenderer srPrimary;
        [SerializeField] private SpriteRenderer srSecondary;

        [Header("DEBUG", order = 100)]
        [SerializeField] private bool FORCE_VALIDATE;

        public ShipStats Stats => stats;
        public Weapon Weapon => weapon;
        public Rigidbody2D Rigidbody => rigidbody;

        public float Health => health;
        public float MaxHealth => maxHealth;

        public bool IsAlive => health > 0f;

        private void OnValidate()
        {
            GameInfo.ValidateMinMax(health, ref maxHealth);

            srPrimary.color = stats.ColorPrimary;
            srSecondary.color = stats.ColorSecondary;

            FORCE_VALIDATE = false;
        }

        public void AddForce()
        {
            if (IsAlive)
            {
                Vector2 velocity = transform.up * stats.MultiplierForce;
                rigidbody.AddForce(velocity);
            }
        }

        public void Rotate(float rotation)
        {
            if (IsAlive)
            {
                float torque = -rotation * stats.MultiplierRotate * Time.deltaTime;
                rigidbody.AddTorque(torque);
            }
        }

        public void Fire()
        {
            if (IsAlive)
            {
                Vector3 posMissile = transform.position;
                posMissile += transform.up * 0.1f;

                Missile missile;
                float angle = (weapon.AngleBetweenShots / 2f) * (weapon.AmountOfShots - 1);

                for (int i = 0; i < weapon.AmountOfShots; ++i)
                {
                    // Projectile rotation
                    Quaternion rotOffset = Quaternion.Euler(0f, 0f, angle);
                    Quaternion rotation = transform.rotation * rotOffset;

                    // Instantiate
                    missile = Instantiate(GameInfo.PrefabMissile, posMissile, rotation);
                    missile.Damage = weapon.Damage;

                    // Set velocity
                    missile.Rigidbody.velocity = missile.transform.up * weapon.ProjectileSpeed;

                    // Destroy after time
                    Destroy(missile.gameObject, weapon.LifetimeMax);

                    // Set for next shot
                    angle -= weapon.AngleBetweenShots;
                }
            }
        }

        public void ApplyDrag(bool drag)
        {
            if (IsAlive)
            {
                rigidbody.drag = drag ? Stats.Drag : 0;
            }
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
                OnDeath();
            }
        }

        protected virtual void OnDeath() { }
    }
}
