using SpaceGame.Ships;
using SpaceGame.Utilities;
using UnityEngine;

namespace SpaceGame.SpaceObjects
{
    public sealed class SpaceRock : SpaceObject
    {
        private SpaceObjectSpawnableInfo settings;
        private bool alive = true;

        private void Start()
        {
            this.settings = (SpaceObjectSpawnableInfo)base.Settings;
        }

        // TODO move triggers to Projectile class
        // protected override void OnTriggerEnter2D(Collider2D collider)
        // {
        //     base.OnTriggerEnter2D(collider);

        //     if (this.alive & collider.tag == GameInfo.TagMissile)
        //     {
        //         Missile missile = collider.GetComponent<Missile>();
        //         this.Scale -= missile.Weapon.MultSpaceRock * this.settings.ScaleMissileDamage;

        //         // If Space Rock too small...
        //         if (this.Scale < this.settings.DestroyBelowScale)
        //         {
        //             this.alive = false;

        //             // Destroy Space Rock
        //             GameInfo.DestroySpaceObject(this);

        //             // Instantiate Item Object
        //             SpaceObjectItem itemObject = (SpaceObjectItem)GameInfo.SpawnSpaceObject(GameInfo.SettingsItemObject, this.transform.position);
        //             itemObject.SetItem(this.settings.RandomItemDrop);
        //         }
        //         else
        //         {
        //             Vector2 force = collider.attachedRigidbody.velocity * this.settings.ScaleMissileImpactForce;
        //             Rigidbody.AddForceAtPosition(force, this.Rigidbody.position);
        //         }
        //     }
        // }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            // TODO move collision to Ship
            Transform transformShip = collision.transform;

            if (transformShip.tag == GameInfo.TagShip || transformShip.tag == GameInfo.TagPlayer)
            {
                float force = collision.rigidbody.velocity.magnitude;
                Ship ship = transformShip.GetComponent<Ship>();
                ship.Damage(force, Enums.DamageType.Collision);
            }
        }
    }
}
