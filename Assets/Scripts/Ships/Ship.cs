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
        [SerializeField] private Weapon weapon;

        [Header("References (as Ship)", order = 90)]
        [SerializeField] private new Rigidbody2D rigidbody;
        [SerializeField] private SpriteRenderer srPrimary;
        [SerializeField] private SpriteRenderer srSecondary;

        [Header("DEBUG", order = 100)]
        [SerializeField] private bool FORCE_VALIDATE;

        private bool isFiring = false;
        private Coroutine routineFiring = null;

        public ShipStats Stats => this.stats;
        public Weapon Weapon => this.weapon;
        public Rigidbody2D Rigidbody => this.rigidbody;

        public float Health => this.health;
        public float MaxHealth => this.maxHealth;

        public bool IsAlive => this.health > 0f;
        public Vector2 Position => this.transform.position;
        public bool IsFiring
        {
            get => this.isFiring;
            set
            {
                this.isFiring = value;

                if (value & this.routineFiring == null)
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
            this.FORCE_VALIDATE = false;
        }

        public float GetRotationToLookAt(Vector2 pos)
        {
            Vector2 posShip = this.Position;
            Vector2 offsetObj = pos - posShip;

            // Debug rays
            if (GameInfo.DO_DEBUG_STUFF)
            {
                Vector2 facingPosition = ((Vector2)this.transform.up * offsetObj.magnitude) + posShip;
                // Draw rays to display in editor
                Debug.DrawLine(posShip, facingPosition, Color.blue);
                Debug.DrawLine(posShip, pos, Color.magenta);
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

        private void Fire()
        {
            if (this.IsAlive)
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

        private IEnumerator RoutineFire()
        {
            while (this.IsFiring)
            {
                this.Fire();
                yield return new WaitForSeconds(this.Weapon.TimeBetweenShots);
            }

            this.routineFiring = null;
        }
    }
}
