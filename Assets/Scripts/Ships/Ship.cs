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
        [SerializeField] private ShipInfo stats;

        [Header("References [Ship]", order = 90)]
        [SerializeField] private new Rigidbody2D rigidbody;
        [SerializeField] private SpriteRenderer srPrimary;
        [SerializeField] private SpriteRenderer srSecondary;
        [SerializeField] private Animator animator;

        [Header("Cheats", order = 95)]
        public bool Invincible;

        [Header("DEBUG", order = 100)]
        [SerializeField] private bool FORCE_VALIDATE;

        private bool isFiring = false;
        private Coroutine routineFiring = null;

        public ShipInfo Stats => this.stats;
        public Rigidbody2D Rigidbody => this.rigidbody;

        public float Health => this.health;
        public float MaxHealth => this.maxHealth;

        public Vector2 Position => this.transform.position;
        public bool Dragging => this.rigidbody.drag == this.stats.Drag;
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

        protected virtual void FixedUpdate()
        {
            this.animator.SetBool("Drag", Dragging);
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
            // TODO if applying drag, make red rectangles on back of ship light up as "braking lights"

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

        public void Damage(float damage, Enums.DamageType damageType)
        {
            if (!Invincible)
            {
                switch (damageType)
                {
                    case Enums.DamageType.Collision: damage *= this.stats.ScaleCollisionDamage; break;
                    case Enums.DamageType.Missile: damage *= this.stats.ScaleMissileDamage; break;
                }

                this.health -= damage;

                if (this.health <= 0f)
                {
                    this.health = 0f;
                    this.OnDeath();
                }
            }
        }

        public abstract ItemWeapon GetWeapon();

        protected virtual void OnDeath() { }

        private void Fire()
        {
            if (this.IsAlive)
            {
                Vector3 posMissile = this.transform.position;
                posMissile += this.transform.up * this.transform.localScale.y;
                ItemWeapon weapon = this.GetWeapon();
                float angle = (weapon.AngleBetweenShots / 2f) * (weapon.AmountOfShots - 1);

                for (int i = 0; i < weapon.AmountOfShots; ++i)
                {
                    // Projectile rotation
                    Quaternion rotOffset = Quaternion.Euler(0f, 0f, angle);
                    Quaternion rotation = this.transform.rotation * rotOffset;

                    // Instantiate
                    Missile.Create(posMissile, rotation, weapon, this);

                    // Set for next missile
                    angle -= weapon.AngleBetweenShots;
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
                Missile missile = collider.GetComponent<Missile>();

                if (missile.SourceShip != this)
                {
                    this.Damage(missile.Weapon.MultShip, Enums.DamageType.Missile);
                    Destroy(collider.gameObject);
                }
            }
        }
    }
}
