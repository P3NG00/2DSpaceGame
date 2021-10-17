using System.Collections;
using SpaceGame.Settings;
using SpaceGame.SpaceObjects;
using SpaceGame.Utilities;
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

        [Header("Cheats", order = 95)]
        public bool Invincible;

        [Header("DEBUG", order = 100)]
        [SerializeField] private bool FORCE_VALIDATE;

        private bool isFiring = false;
        private Coroutine routineFiring = null;

        public ShipStats Stats => this.stats;
        public Rigidbody2D Rigidbody => this.rigidbody;

        public float Health => this.health;
        public float MaxHealth => this.maxHealth;

        public Vector2 Position => this.transform.position;
        public bool IsAlive => this.health > 0f;
        public bool IsFiring
        {
            get => this.isFiring;
            set
            {
                this.isFiring = value;

                if (value & GetWeapon() != null & this.routineFiring == null)
                {
                    this.routineFiring = StartCoroutine(this.RoutineFire());
                }
            }
        }

        private void OnValidate()
        {
            Util.ValidateMinMax(this.health, ref this.maxHealth);
            this.srPrimary.color = this.stats.ColorPrimary;
            this.srSecondary.color = this.stats.ColorSecondary;

            if (this.FORCE_VALIDATE)
            {
                this.FORCE_VALIDATE = false;
                print("FORCE VALIDATE SUCCESSFUL");
            }
        }

        public float GetRotationToLookAt(Vector2 pos)
        {
            Vector2 posShip = this.Position;
            Vector2 offsetObj = pos - posShip;

            // Debug rays
            if (GameInfo.DEBUG_RAYS)
            {
                Vector2 facingPosition = ((Vector2)this.transform.up * offsetObj.magnitude) + posShip;
                // Draw rays to display in editor
                Debug.DrawLine(posShip, facingPosition, Color.red);
                Debug.DrawLine(posShip, pos, Color.green);
            }

            return Vector2.Dot(offsetObj.normalized, this.transform.right);
        }

        public void RotateToLookAt(Vector2 pos) => this.Rotate(this.GetRotationToLookAt(pos));

        public void ApplyForce()
        {
            if (this.IsAlive)
            {
                Vector2 v = this.transform.up * this.stats.MultiplierForce;
                this.rigidbody.AddForce(v);
            }
        }

        public void Rotate(float rotation)
        {
            if (this.IsAlive)
            {
                float r = -rotation * this.stats.MultiplierRotate * Time.deltaTime;
                this.rigidbody.AddTorque(r);
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

        public void Damage(float damage, DamageType damageType)
        {
            if (!Invincible)
            {
                switch (damageType)
                {
                    case DamageType.Collision: damage *= this.stats.ScaleCollisionDamage; break;
                    case DamageType.Missile: damage *= this.stats.ScaleMissileDamage; break;
                }

                this.health -= damage;

                if (this.health <= 0f)
                {
                    this.health = 0f;
                    this.OnDeath();
                }
            }
        }

        public abstract Weapon GetWeapon();

        protected virtual void OnDeath() { }

        private void Fire()
        {
            if (this.IsAlive)
            {
                Vector3 posMissile = this.transform.position;
                posMissile += this.transform.up * this.transform.localScale.y;
                float angle = (this.GetWeapon().AngleBetweenShots / 2f) * (this.GetWeapon().AmountOfShots - 1);

                for (int i = 0; i < this.GetWeapon().AmountOfShots; ++i)
                {
                    // Projectile rotation
                    Quaternion rotOffset = Quaternion.Euler(0f, 0f, angle);
                    Quaternion rotation = this.transform.rotation * rotOffset;

                    // Instantiate
                    Missile missile = Instantiate(GameInfo.PrefabMissile, posMissile, rotation);
                    missile.Weapon = this.GetWeapon();

                    // Set velocity
                    missile.Rigidbody.velocity = missile.transform.up * this.GetWeapon().ProjectileSpeed;

                    // Destroy after time
                    Destroy(missile.gameObject, this.GetWeapon().LifetimeMax);

                    // Set for next missile
                    angle -= this.GetWeapon().AngleBetweenShots;
                }
            }
        }

        private IEnumerator RoutineFire()
        {
            while (this.IsFiring)
            {
                this.Fire();
                yield return new WaitForSeconds(this.GetWeapon().TimeBetweenShots);
            }

            this.routineFiring = null;
        }

        private void OnTriggerEnter2D(Collider2D collider)
        {
            if (collider.tag == GameInfo.TagMissile)
            {
                Destroy(collider.gameObject);
                Missile missile = collider.GetComponent<Missile>();
                this.Damage(missile.Weapon.MultShip, DamageType.Missile);
            }
        }

        public enum DamageType
        {
            Collision,
            Missile,
        }
    }
}
