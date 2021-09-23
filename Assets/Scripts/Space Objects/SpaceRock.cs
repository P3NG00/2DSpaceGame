using UnityEngine;

namespace SpaceGame.SpaceObjects
{
    public sealed class SpaceRock : SpaceObject
    {
        protected override void OnTriggerEnter2D(Collider2D collider)
        {
            base.OnTriggerEnter2D(collider);

            if (collider.tag == GameInfo.TagMissile)
            {
                Scale -= Settings.ScaleMissileImpactStep;
                int reward = 1;

                if (Scale < Settings.MinScale)
                {
                    // Give bonus credit
                    ++reward;

                    // Destroy Space Rock
                    GameInfo.DestroySpaceObject(this);

                    // Instantiate Item Object
                    ItemObject itemObject = (ItemObject)GameInfo.SpawnSpaceObject(GameInfo.SettingsItemObject, transform.position);
                    itemObject.SetInfo(Settings.RandomItemDrop, Settings.RandomDropAmount);
                }
                else
                {
                    Rigidbody2D otherRigidbody = collider.gameObject.GetComponent<Rigidbody2D>();
                    Vector2 force = otherRigidbody.velocity * Settings.ScaleMissileImpactForce;
                    Vector2 position = Rigidbody.position;
                    Rigidbody.AddForceAtPosition(force, position);
                }

                // Give player credit(s)
                Vector2 pos = transform.position;
                GameInfo.GiveCredits(reward, pos);
            }
        }

        protected override void OnCollisionEnter2D(Collision2D collision)
        {
            base.OnCollisionEnter2D(collision);

            // TODO Space Rock / Player collision - add damage, effect, other?
            // ideas
            //  hurt player in relation to velocity
            //  create collision particle system effect
            //  audio feedback
        }
    }
}
