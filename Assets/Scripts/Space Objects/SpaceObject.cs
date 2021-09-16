using SpaceGame.Settings;
using UnityEngine;

namespace SpaceGame.SpaceObjects
{
    public abstract class SpaceObject : MonoBehaviour
    {
        [Header("Info (as SpaceObject)", order = 5)]
        [SerializeField] private SpaceObjectSettings settings;

        [Header("References (as SpaceObject)", order = 99)]
        [SerializeField] private new Rigidbody2D rigidbody;
        [SerializeField] private SpriteRenderer spriteRenderer;

        public SpaceObjectSettings Settings => settings;
        public SpriteRenderer SpriteRenderer => spriteRenderer;
        public Rigidbody2D Rigidbody => rigidbody;

        public float Scale
        {
            get => transform.localScale.x;
            set
            {
                transform.localScale = Vector2.one * value;
            }
        }

        private void Start()
        {
            spriteRenderer.color = GetColor();
        }

        protected void OnTriggerEnter2D(Collider2D collider)
        {
            OnTriggered(collider);
        }

        protected void OnCollisionEnter2D(Collision2D collision)
        {
            OnCollided(collision);
        }

        protected virtual Color GetColor() => settings.Color;

        protected virtual void OnTriggered(Collider2D collider) { }

        protected virtual void OnCollided(Collision2D collision) { }
    }
}
