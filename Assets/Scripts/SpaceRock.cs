using UnityEngine;

namespace SpaceGame
{
    public sealed class SpaceRock : MonoBehaviour
    {
        [Header("Info", order = 0)]
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
            get => transform.localScale.x;
            set
            {
                transform.localScale = Vector2.one * value;
            }
        }

        private void OnTriggerEnter2D(Collider2D collider)
        {
            GameObject otherObj = collider.gameObject;

            if (otherObj.tag == "Missile")
            {
                Scale -= 0.5f;
                int reward = 1;
                Destroy(otherObj);

                if (Scale < GameInfo.GMSettings.MinSpaceRockScale)
                {
                    ++reward;
                    Destroy(gameObject);
                }
                else
                {
                    Rigidbody2D otherRigidbody = otherObj.GetComponent<Rigidbody2D>();
                    Vector2 force = otherRigidbody.velocity * GameInfo.GMSettings.ScaleMissileImpactForce;
                    Vector2 position = otherObj.transform.position;
                    rigidbody.AddForceAtPosition(force, position);
                }

                Vector2 pos = transform.position;
                GameInfo.GiveCredits(reward, pos);
            }
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.tag == "Player")
            {
                // TODO Space Rock / Player collision - add damage, effect, other?
                // ideas
                //  hurt player in relation to velocity
                //  create collision particle system effect
                //  audio feedback
            }
        }

        public void SetVelocities(Vector2 velocity, float angularVelocity)
        {
            rigidbody.velocity = velocity;
            rigidbody.angularVelocity = angularVelocity;
        }
    }
}
