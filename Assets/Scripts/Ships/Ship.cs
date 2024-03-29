using System.Collections;
using SpaceGame.Items;
using SpaceGame.SpaceObjects;
using SpaceGame.UI;
using SpaceGame.Utilities;
using UnityEngine;

namespace SpaceGame.Ships
{
    public abstract class Ship : MonoBehaviour
    {
        [Header("Info", order = 0)]
        [SerializeField] private float health;
        [SerializeField] private float maxHealth;
        [SerializeField] private ShipInfo shipInfo;

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

        // Public cache
        private GameObject itemObjectToDestroy = null;

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
                ItemInfo itemInfo = this.GetItemInfo();

                if (value && this.IsAlive && !this.IsFiring && itemInfo != null && itemInfo is ItemWeapon itemWeapon)
                {
                    this.isFiring = true;
                    this.routineFiring = StartCoroutine(itemWeapon.UseRoutine(this));
                }
                else if (!value)
                {
                    if (this.routineFiring != null)
                    {
                        StopCoroutine(this.routineFiring);
                    }

                    this.routineFiring = null;
                    this.isFiring = false;

                    if (this.itemObjectToDestroy != null)
                    {
                        Destroy(this.itemObjectToDestroy);
                        this.itemObjectToDestroy = null;
                    }
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
                Vector2 velocity = this.transform.up * this.shipInfo.MultiplierForce * Time.deltaTime;
                this.rigidbody.AddForce(velocity, ForceMode2D.Impulse);
                velocity = this.rigidbody.velocity;

                if (velocity.magnitude > this.shipInfo.MaxMagnitude)
                {
                    velocity = this.rigidbody.velocity.normalized * this.shipInfo.MaxMagnitude;
                    this.rigidbody.velocity = velocity;
                }
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
            if (this.IsAlive)
            {
                this.rigidbody.drag = drag ? this.ShipInfo.Drag : 0f;
            }
        }

        public void Heal(float amount)
        {
            if (amount > 0)
            {
                this.health += amount;

                if (this.health > this.maxHealth)
                {
                    this.health = this.maxHealth;
                }
            }
            else if (GameInfo.DEBUG_LOG)
            {
                print($"Cannot heal for amount of '{amount}'");
            }
        }

        public void Damage(float damage, Enums.DamageType damageType)
        {
            if (!Invincible)
            {
                switch (damageType)
                {
                    case Enums.DamageType.Collision: damage *= this.shipInfo.ScaleCollisionDamage; break;
                    case Enums.DamageType.Weapon: damage *= this.shipInfo.ScaleMissileDamage; break;
                }

                this.health -= damage;
                this.OnDamage();

                if (this.health <= 0f)
                {
                    this.health = 0f;
                    this.OnDeath();
                }
            }
        }

        public void UseWeapon()
        {
            if (this.IsAlive)
            {
                this.itemObjectToDestroy = ((ItemWeapon)this.GetItemInfo()).UseWeapon(this);
                UIInventorySlot itemWeaponSlot = this.ItemWeaponSlot();

                if (itemWeaponSlot != null && !itemWeaponSlot.ItemStack.ItemInfo.Infinite)
                {
                    itemWeaponSlot.ItemStack.ModifyAmount(-1);
                    itemWeaponSlot.UpdateText();
                }
            }
        }

        public void StartUsingRoutine(IEnumerator itemRoutine)
        {
            if (!this.IsUsing)
            {
                this.routineUsing = StartCoroutine(itemRoutine);
            }
        }

        public void StopUsingRoutine()
        {
            if (this.IsUsing)
            {
                this.StopCoroutine(this.routineUsing);
                this.routineUsing = null;
            }
        }

        public abstract ItemInfo GetItemInfo();
        public virtual UIInventorySlot ItemWeaponSlot() => null;

        protected virtual void OnDeath() { }
        protected virtual void OnDamage() { }

        // TODO remove?
        // private IEnumerator RoutineFire()
        // {
        //     ItemInfo itemInfo = this.GetItemInfo();

        //     while (this.IsFiring & itemInfo != null)
        //     {
        //         this.UseWeapon();
        //         yield return new WaitForSeconds(itemInfo.TimeBetweenUses);
        //     }

        //     this.IsFiring = false;
        // }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.tag == GameInfo.TagSpaceRock)
            {
                float magnitude = collision.relativeVelocity.magnitude;
                this.Damage(magnitude, Enums.DamageType.Collision);
                collision.gameObject.GetComponent<SpaceObject>().Damage(magnitude / 100f, Enums.DamageType.Collision); // TODO change scale of space rock damage?
            }
        }
    }
}
