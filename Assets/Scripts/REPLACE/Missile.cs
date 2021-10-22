using SpaceGame.Settings;
using SpaceGame.Ships;
using UnityEngine;

namespace SpaceGame.SpaceObjects
{
    public class Missile : MonoBehaviour
    {
        // TODO turn into Projectile, get rid of 'Missile'

        [Header("Info", order = 0)]
        [SerializeField] private ItemWeapon weapon;

        [Header("References", order = 99)]
        [SerializeField] private new Rigidbody2D rigidbody;
        [SerializeField] private SpriteRenderer spriteRenderer;

        private Ship sourceShip = null;

        public Ship SourceShip => this.sourceShip;

        public ItemWeapon Weapon
        {
            get => this.weapon;
            set
            {
                this.weapon = value;
                this.spriteRenderer.color = this.weapon.Color;
            }
        }

        public Rigidbody2D Rigidbody => this.rigidbody;

        public static Missile Create(Vector2 position, Quaternion rotation, ItemWeapon weapon, Ship source)
        {
            Missile missile = Instantiate(GameInfo.PrefabMissile, position, rotation);
            missile.sourceShip = source;
            missile.Weapon = weapon;
            missile.Rigidbody.velocity = missile.transform.up * weapon.ProjectileSpeed;
            Destroy(missile.gameObject, weapon.LifetimeMax);
            return missile;
        }
    }
}
