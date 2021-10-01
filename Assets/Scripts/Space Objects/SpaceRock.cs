using SpaceGame.Settings;
using SpaceGame.Ships;
using UnityEngine;

namespace SpaceGame.SpaceObjects
{
    public sealed class SpaceRock : SpaceObject
    {
        private SpaceObjectSpawnableSettings settings;

        private void Start()
        {
            settings = (SpaceObjectSpawnableSettings)base.Settings;
        }

        protected override void OnTriggerEnter2D(Collider2D collider)
        {
            base.OnTriggerEnter2D(collider);

            if (collider.tag == GameInfo.TagMissile)
            {
                Missile missile = collider.GetComponent<Missile>();
                Scale -= missile.Weapon.MultSpaceRock * settings.ScaleMissileDamage;
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
                    itemObject.Item = drop.ItemInfo;
                    itemObject.Amount = drop.RandomAmount;
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

        private void OnCollisionEnter2D(Collision2D collision)
        {
            Transform transformShip = collision.transform;

            if (transformShip.tag == GameInfo.TagShip || transformShip.tag == GameInfo.TagPlayer)
            {
                // TODO mess with damage from collision
                float force = collision.rigidbody.velocity.magnitude;

                Ship ship = transformShip.GetComponent<Ship>();
                ship.Damage(force);
            }
        }
    }
}
