using SpaceGame.Items;
using SpaceGame.Settings;
using SpaceGame.Ships;
using UnityEngine;

namespace SpaceGame.SpaceObjects
{
    public sealed class SpaceRock : SpaceObject
    {
        private SpaceObjectSpawnableSettings settings;
        private bool alive = true;

        private void Start()
        {
            this.settings = (SpaceObjectSpawnableSettings)base.Settings;
        }

        protected override void OnTriggerEnter2D(Collider2D collider)
        {
            base.OnTriggerEnter2D(collider);

            if (this.alive & collider.tag == GameInfo.TagMissile)
            {
                Missile missile = collider.GetComponent<Missile>();
                this.Scale -= missile.Weapon.MultSpaceRock * this.settings.ScaleMissileDamage;

                // If Space Rock too small...
                if (this.Scale < this.settings.DestroyBelowScale)
                {
                    this.alive = false;

                    // Destroy Space Rock
                    GameInfo.DestroySpaceObject(this);

                    // Instantiate Item Object
                    ItemObject itemObject = (ItemObject)GameInfo.SpawnSpaceObject(GameInfo.SettingsItemObject, this.transform.position);
                    itemObject.SetItem(this.settings.RandomItemDrop);
                }
                else
                {
                    Vector2 force = collider.attachedRigidbody.velocity * this.settings.ScaleMissileImpactForce;
                    Rigidbody.AddForceAtPosition(force, this.Rigidbody.position);
                }
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
