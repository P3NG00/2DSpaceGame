using UnityEngine;

namespace SpaceGame.SpaceObjects
{
    public sealed class SpaceRock : SpaceObject
    {
        protected override void OnTriggered(Collider2D collider)
        {
            if (collider.tag == "Missile")
            {
                Destroy(collider.gameObject);
                Scale -= Settings.ScaleMissileImpactStep;
                int reward = 1;

                if (Scale < Settings.MinScale)
                {
                    ++reward;
                    Destroy(gameObject);
                    ItemObject itemObject = (ItemObject)GameInfo.SpawnSpaceObject(GameInfo.SettingsItemObject, transform.position);
                    itemObject.SetInfo(Settings.RandomItemDrop, Random.Range(0, 5));
                }
                else
                {
                    Rigidbody2D otherRigidbody = collider.gameObject.GetComponent<Rigidbody2D>();
                    Vector2 force = otherRigidbody.velocity * Settings.ScaleMissileImpactForce;
                    Vector2 position = Rigidbody.position;
                    Rigidbody.AddForceAtPosition(force, position);
                }

                Vector2 pos = transform.position;
                GameInfo.GiveCredits(reward, pos);
            }
        }

        protected override void OnCollided(Collision2D collision)
        {
            // TODO Space Rock / Player collision - add damage, effect, other?
            // ideas
            //  hurt player in relation to velocity
            //  create collision particle system effect
            //  audio feedback
        }
    }
}
