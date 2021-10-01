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

        public ShipStats Stats => this.stats;
        public Weapon Weapon => this.weapon;
        public Rigidbody2D Rigidbody => this.rigidbody;

        public float Health => this.health;
        public float MaxHealth => this.maxHealth;

        public bool IsAlive => this.health > 0f;

        private void OnValidate()
        {
            GameInfo.ValidateMinMax(this.health, ref this.maxHealth);

            this.srPrimary.color = this.stats.ColorPrimary;
            this.srSecondary.color = this.stats.ColorSecondary;

            this.FORCE_VALIDATE = false;
        }

        public void AddForce()
        {
            if (this.IsAlive)
            {
                Vector2 velocity = this.transform.up * this.stats.MultiplierForce;
                this.rigidbody.AddForce(velocity);
            }
        }

        public void Rotate(float rotation)
        {
            if (this.IsAlive)
            {
                float torque = -rotation * this.stats.MultiplierRotate * Time.deltaTime;
                this.rigidbody.AddTorque(torque);
            }
        }

        public void Fire()
        {
            if (IsAlive)
            {
                Vector3 posMissile = this.transform.position;
                posMissile += this.transform.up * 0.1f;
                float angle = (this.weapon.AngleBetweenShots / 2f) * (this.weapon.AmountOfShots - 1);

                for (int i = 0; i < this.weapon.AmountOfShots; ++i)
                {
                    // Projectile rotation
                    Quaternion rotOffset = Quaternion.Euler(0f, 0f, angle);
                    Quaternion rotation = this.transform.rotation * rotOffset;

                    // Instantiate
                    Missile missile = Instantiate(GameInfo.PrefabMissile, posMissile, rotation);
                    missile.Weapon = this.weapon;

                    // Set velocity
                    missile.Rigidbody.velocity = missile.transform.up * this.weapon.ProjectileSpeed;

                    // Destroy after time
                    Destroy(missile.gameObject, this.weapon.LifetimeMax);

                    // Set for next missile
                    angle -= this.weapon.AngleBetweenShots;
                }
            }
        }

        public void ApplyDrag(bool drag)
        {
            if (this.IsAlive)
            {
                this.rigidbody.drag = drag ? this.Stats.Drag : 0f;
            }
        }

        public void Heal(float amount)
        {
            this.health += amount;

            if (this.health > this.maxHealth)
            {
                this.health = this.maxHealth;
            }
        }

        public void Damage(float damage)
        {
            this.health -= damage;

            if (this.health <= 0f)
            {
                this.health = 0f;
                this.OnDeath();
            }
        }

        protected virtual void OnDeath() { }
    }
}
