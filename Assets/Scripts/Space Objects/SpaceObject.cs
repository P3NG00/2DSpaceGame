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

        public SpriteRenderer SpriteRenderer => this.spriteRenderer;
        public Rigidbody2D Rigidbody => this.rigidbody;

        public float Scale
        {
            get => this.transform.localScale.x;
            set => this.transform.localScale = Vector2.one * value;
        }

        private void Start()
        {
            this.SpriteRenderer.color = this.GetColor();
        }

        protected virtual Color GetColor() => this.Settings.Color;

        protected virtual void OnTriggerEnter2D(Collider2D collider)
        {
            if (this.Settings.DestroyMissile & collider.tag == GameInfo.TagMissile)
            {
                Destroy(collider.gameObject);
            }
        }
    }
}
