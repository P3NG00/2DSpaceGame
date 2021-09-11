using UnityEngine;

namespace SpaceGame.SpaceObjects
{
    public sealed class SpaceRock : SpaceObject
    {
        protected override void OnMissileHit(Collider2D collider)
        {
            Scale -= 0.5f;
            int reward = 1;
            Destroy(collider.gameObject);

            if (Scale < GameInfo.GMSettings.MinSpaceRockScale)
            {
                ++reward;
                Destroy(gameObject);
            }
            else
            {
                Rigidbody2D otherRigidbody = collider.gameObject.GetComponent<Rigidbody2D>();
                Vector2 force = otherRigidbody.velocity * GameInfo.GMSettings.ScaleMissileImpactForce;
                Vector2 position = collider.gameObject.transform.position;
                rigidbody.AddForceAtPosition(force, position);
            }

            Vector2 pos = transform.position;
            GameInfo.GiveCredits(reward, pos);
        }

        protected override void OnCollideWithPlayer(Collision2D collision)
        {
            // TODO Space Rock / Player collision - add damage, effect, other?
            // ideas
            //  hurt player in relation to velocity
            //  create collision particle system effect
            //  audio feedback
        }

        public void SetVelocities(Vector2 velocity, float angularVelocity)
        {
            rigidbody.velocity = velocity;
            rigidbody.angularVelocity = angularVelocity;
        }
    }
}
