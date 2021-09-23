using SpaceGame.Settings;
using UnityEngine;

namespace SpaceGame.SpaceObjects
{
    public abstract class SpaceObject : MonoBehaviour
    {
        [Header("Info (as SpaceObject)", order = 5)]
        public SpaceObjectSettings Settings;

        [Header("References (as SpaceObject)", order = 99)]
        [SerializeField] private new Rigidbody2D rigidbody;
        [SerializeField] private SpriteRenderer spriteRenderer;

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

        protected virtual Color GetColor() => Settings.Color;

        protected virtual void OnTriggerEnter2D(Collider2D collider)
        {
            if (collider.tag == GameInfo.TagMissile)
            {
                Destroy(collider.gameObject);
            }
        }

        protected virtual void OnCollisionEnter2D(Collision2D collision) { }
    }
}
