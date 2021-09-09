using UnityEngine;

namespace Project
{
    public sealed class SpaceRock : MonoBehaviour
    {
        [Header("Info", order = 0)]
        [SerializeField, Min(0f)] private float scaleSize;
        [SerializeField, Min(0f)] private float scaleMass;

        [Header("References", order = 99)]
        [SerializeField] private new Rigidbody2D rigidbody;

        public float Scale
        {
            get => scaleSize;
            set => scaleSize = value;
        }

        private void Update()
        {
            if (scaleSize >= GameInfo.MinSpaceRockScale)
            {
                transform.localScale = Vector2.one * scaleSize;
                rigidbody.mass = scaleSize * scaleMass;
            }
            else
            {
                Destroy(gameObject);
                GameInfo.IncrementCredit();
            }
        }

        private void OnTriggerEnter2D(Collider2D collider)
        {
            if (collider.gameObject.tag == "Missile")
            {
                Scale -= 0.5f;
                GameInfo.IncrementCredit();
                Destroy(collider.gameObject);
            }
        }

        public void SetVelocities(Vector2 velocity, float angularVelocity)
        {
            rigidbody.velocity = velocity;
            rigidbody.angularVelocity = angularVelocity;
        }
    }
}
