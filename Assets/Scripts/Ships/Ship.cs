using System;
using System.Globalization;
using System.Collections;
using SpaceGame.Items;
using SpaceGame.Projectiles;
using SpaceGame.UI;
using SpaceGame.Utilities;
using UnityEngine;
using UnityEngine.Serialization;

namespace SpaceGame.Ships
{
    public abstract class Ship : MonoBehaviour
    {
        [Header("Info", order = 0)]
        [SerializeField] private float health;
        [SerializeField] private float maxHealth;
        [FormerlySerializedAs("stats"), SerializeField] private ShipInfo shipInfo;

        [Header("References [Ship]", order = 90)]
        [SerializeField] private new Rigidbody2D rigidbody;
        [SerializeField] private SpriteRenderer srPrimary;
        [SerializeField] private SpriteRenderer srSecondary;
        [SerializeField] private Animator animator;

        [Header("Cheats", order = 95)]
        public bool Invincible;

        [Header("DEBUG", order = 100)]
        [SerializeField] private bool FORCE_VALIDATE;

        // Private caches
        private bool isFiring = false;
        private Coroutine routineFiring = null;
        private Coroutine routineUsing = null;

        // Public getters
        public float Health => this.health;
        public float MaxHealth => this.maxHealth;
        public ShipInfo ShipInfo => this.shipInfo;
        public Rigidbody2D Rigidbody => this.rigidbody;

        // Public properties
        public Vector2 Position => this.transform.position;
        public bool IsAlive => this.health > 0f;
        public bool IsUsing => this.routineUsing != null;
        public bool IsFiring
        {
            get => this.isFiring;
            set
            {
                this.isFiring = value;

                if (value & GetProjectile() != null & this.routineFiring == null)
                {
                    this.routineFiring = StartCoroutine(this.RoutineFire());
                }
            }
        }

        private void OnValidate()
        {
            Util.ValidateMinMax(this.health, ref this.maxHealth);
            this.srPrimary.color = this.shipInfo.ColorPrimary;
            this.srSecondary.color = this.shipInfo.ColorSecondary;

            if (this.FORCE_VALIDATE)
            {
                this.FORCE_VALIDATE = false;
                print("FORCE VALIDATE SUCCESSFUL");
            }
        }

        protected virtual void FixedUpdate()
        {
            this.animator.SetBool("Drag", this.rigidbody.drag == this.shipInfo.Drag);
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
                Vector2 v = this.transform.up * this.shipInfo.MultiplierForce;
                this.rigidbody.AddForce(v);
            }
        }

        public void Rotate(float rotation)
        {
            if (this.IsAlive)
            {
                float r = -rotation * this.shipInfo.MultiplierRotate * Time.deltaTime;
                this.rigidbody.AddTorque(r);
            }
        }

        public void ApplyDrag(bool drag)
        {
            // TODO if applying drag, make red rectangles on back of ship light up as "braking lights"

            if (this.IsAlive)
            {
                this.rigidbody.drag = drag ? this.ShipInfo.Drag : 0f;
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
                    case Enums.DamageType.Collision: damage *= this.shipInfo.ScaleCollisionDamage; break;
                    case Enums.DamageType.Projectile: damage *= this.shipInfo.ScaleMissileDamage; break;
                }

                this.health -= damage;

                if (this.health <= 0f)
                {
                    this.health = 0f;
                    this.OnDeath();
                }
            }
        }

        public void StartItemRoutine(IEnumerator itemRoutine)
        {
            if (!this.IsUsing)
            {
                this.routineUsing = StartCoroutine(itemRoutine);
            }
        }

        public void StopItemRoutine()
        {
            if (this.IsUsing)
            {
                this.StopCoroutine(this.routineUsing);
                this.routineUsing = null;
            }
        }

        public virtual UIInventorySlot ItemWeaponSlot() => null;

        public abstract ItemInfoProjectile GetProjectile();

        protected virtual void OnDeath() { }

        private void Fire()
        {
            if (this.IsAlive)
            {
                Projectile.Create(this.GetProjectile().ProjectileInfo, this);
                UIInventorySlot itemWeaponSlot = this.ItemWeaponSlot();

                if (itemWeaponSlot != null && !itemWeaponSlot.ItemStack.ItemInfo.Infinite)
                {
                    itemWeaponSlot.ItemStack.Amount--;
                    itemWeaponSlot.UpdateText();
                }

                // TODO reimplement later - code used for multi-shot weapons
                // float angle = (weapon.AngleBetweenShots / 2f) * (weapon.AmountOfShots - 1);

                // for (int i = 0; i < weapon.AmountOfShots; ++i)
                // {
                //     // Projectile rotation
                //     Quaternion rotOffset = Quaternion.Euler(0f, 0f, angle);
                //     Quaternion rotation = this.transform.rotation * rotOffset;

                //     // Instantiate
                //     Missile.Create(pos, rotation, weapon, this);

                //     // Set for next missile
                //     angle -= weapon.AngleBetweenShots;
                // }
            }
        }

        private IEnumerator RoutineFire()
        {
            while (this.IsFiring & this.GetProjectile() != null)
            {
                this.Fire();
                yield return new WaitForSeconds(this.GetProjectile().TimeBetweenShots);
            }

            this.routineFiring = null;
        }

        // TODO remove
        // Missile class doesn't exist anymore, use Projectile instead
        // Projectile damage should be dealt with in the Projectile's OnTriggerEnter function
        // private void OnTriggerEnter2D(Collider2D collider)
        // {
        //     if (collider.tag == GameInfo.TagMissile)
        //     {
        //         Missile missile = collider.GetComponent<Missile>();

        //         if (missile.SourceShip != this)
        //         {
        //             this.Damage(missile.Weapon.MultShip, Enums.DamageType.Projectile);
        //             Destroy(collider.gameObject);
        //         }
        //     }
        // }
    }
}
