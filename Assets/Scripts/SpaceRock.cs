using UnityEngine;

namespace Project
{
    public sealed class SpaceRock : MonoBehaviour
    {
        [Header("Info", order = 0)]
        [SerializeField, Min(0f)] private float scaleSize;
        [SerializeField, Min(0f)] private float scaleMass;
        [SerializeField] private Color color;

        [Header("References", order = 99)]
        [SerializeField] private new Rigidbody2D rigidbody;
        [SerializeField] private SpriteRenderer spriteRenderer;

        private void OnValidate()
        {
            spriteRenderer.color = color;
        }

        public float Scale
        {
            get => scaleSize;
            set
            {
                scaleSize = value;

                transform.localScale = Vector2.one * scaleSize;
                rigidbody.mass = scaleSize * scaleMass;
            }
        }

        private void OnTriggerEnter2D(Collider2D collider)
        {
            if (collider.gameObject.tag == "Missile")
            {
                Scale -= 0.5f;
                int reward = 1;
                Destroy(collider.gameObject);

                if (Scale < GameInfo.GMSettings.MinSpaceRockScale)
                {
                    ++reward;
                    Destroy(gameObject);
                }

                Vector2 pos = transform.position;
                GameInfo.GiveCredits(reward, pos);
            }
        }

        public void SetVelocities(Vector2 velocity, float angularVelocity)
        {
            rigidbody.velocity = velocity;
            rigidbody.angularVelocity = angularVelocity;
        }
    }
}
