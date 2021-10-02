using SpaceGame.Settings;
using UnityEngine;

namespace SpaceGame.SpaceObjects
{
    public class Missile : MonoBehaviour
    {
        [Header("Info", order = 0)]
        private Weapon weapon;

        [Header("References", order = 99)]
        [SerializeField] private new Rigidbody2D rigidbody;
        [SerializeField] private SpriteRenderer spriteRenderer;

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
    }
}
