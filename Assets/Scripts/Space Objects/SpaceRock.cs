using SpaceGame.Settings;
using UnityEngine;

namespace SpaceGame.SpaceObjects
{
    public sealed class SpaceRock : SpaceObject
    {
        private SpaceObjectSpawnableSettings settings;

        private void Start()
        {
            this.settings = (SpaceObjectSpawnableSettings)base.Settings;
        }

        protected override void OnTriggerEnter2D(Collider2D collider)
        {
            base.OnTriggerEnter2D(collider);

            if (collider.tag == GameInfo.TagMissile)
            {
                Scale -= settings.ScaleMissileImpactStep;
                int reward = 1;

                // If Space Rock too small...
                if (Scale < settings.MinScale)
                {
                    // Give bonus credit
                    ++reward;

                    // Destroy Space Rock
                    GameInfo.DestroySpaceObject(this);

                    // Instantiate Item Object
                    ItemObject itemObject = (ItemObject)GameInfo.SpawnSpaceObject(GameInfo.SettingsItemObject, transform.position);
                    ItemDrop drop = settings.RandomItemDrop;
                    itemObject.SetInfo(drop.ItemInfo, drop.RandomAmount);
                }
                else
                {
                    Rigidbody2D otherRigidbody = collider.gameObject.GetComponent<Rigidbody2D>();
                    Vector2 force = otherRigidbody.velocity * settings.ScaleMissileImpactForce;
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
