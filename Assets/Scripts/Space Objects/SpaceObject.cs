using SpaceGame.Settings;
using UnityEngine;

namespace SpaceGame.SpaceObjects
{
    public abstract class SpaceObject : MonoBehaviour
    {
        [Header("Info (as SpaceObject)", order = 5)]
        [SerializeField] private SpaceObjectSettings settings;

        [Header("References (as SpaceObject)", order = 99)]
        [SerializeField] protected new Rigidbody2D rigidbody;

        public float Scale
        {
            get => transform.localScale.x;
            set
            {
                transform.localScale = Vector2.one * value;
            }
        }

        public void SetVelocities(Vector2 velocity, float angularVelocity)
        {
            rigidbody.velocity = velocity;
            rigidbody.angularVelocity = angularVelocity;
        }

        private void OnTriggerEnter2D(Collider2D collider)
        {
            if (collider.gameObject.tag == "Missile")
            {
                OnMissileHit(collider);
            }
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.tag == "Player")
            {
                OnCollideWithPlayer(collision);
            }
        }

        protected virtual void OnMissileHit(Collider2D collider) { }

        protected virtual void OnCollideWithPlayer(Collision2D collision) { }
    }
}
