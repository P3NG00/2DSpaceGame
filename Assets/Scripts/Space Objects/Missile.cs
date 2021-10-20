using SpaceGame.Settings;
using SpaceGame.Ships;
using UnityEngine;

namespace SpaceGame.SpaceObjects
{
    public class Missile : MonoBehaviour
    {
        [Header("Info", order = 0)]
        [SerializeField] private Weapon weapon;

        [Header("References", order = 99)]
        [SerializeField] private new Rigidbody2D rigidbody;
        [SerializeField] private SpriteRenderer spriteRenderer;

        private Ship sourceShip;

        public Ship SourceShip => this.sourceShip;

        public Weapon Weapon
        {
            get => this.weapon;
            set
            {
                this.weapon = value;
                this.spriteRenderer.color = this.weapon.Color;
            }
        }

        public Rigidbody2D Rigidbody => this.rigidbody;

        public static Missile Create(Vector2 position, Quaternion rotation, Weapon weapon, Ship source)
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
