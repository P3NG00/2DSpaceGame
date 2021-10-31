using SpaceGame.Ships;
using SpaceGame.SpaceObjects;
using UnityEngine;

namespace SpaceGame.Projectiles
{
    public sealed class Projectile : MonoBehaviour
    {
        [Header("References [Projectile]", order = 95)]
        [SerializeField] private new Rigidbody2D rigidbody;
        [SerializeField] private SpriteRenderer spriteRenderer;

        private ProjectileInfo projectileInfo;
        private Ship sourceShip;

        private bool alive = true;

        public static Projectile Create(ProjectileInfo projectileInfo, Ship source)
        {
            Projectile projectile = Instantiate(projectileInfo.ProjectileObject, source.transform.position, source.transform.rotation);
            projectile.rigidbody.velocity = source.transform.up * projectileInfo.Magnitude;
            projectile.spriteRenderer.sprite = projectileInfo.CorrespondingItem.Sprite;
            projectile.spriteRenderer.color = projectileInfo.CorrespondingItem.Color;
            projectile.projectileInfo = projectileInfo;
            projectile.sourceShip = source;
            Destroy(projectile.gameObject, projectileInfo.Lifetime);
            return projectile;
        }

        private void OnTriggerEnter2D(Collider2D collider)
        {
            if (this.alive)
            {
                // Unable to use switch statement for this because it uses non-constant values
                if (collider.tag == GameInfo.TagShip || collider.tag == GameInfo.TagPlayer)
                {
                    // Damage ship if not source
                    Ship ship = collider.GetComponent<Ship>();

                    if (this.sourceShip != ship)
                    {
                        ship.Damage(this.projectileInfo.DamageShip, Enums.DamageType.Weapon);
                        DestroyProjectile();
                    }
                }
                else if (collider.tag == GameInfo.TagSpaceRock)
                {
                    // Decrease size of Space Rock
                    collider.GetComponent<SpaceObject>().Damage(this.projectileInfo.DamageSpaceObject, Enums.DamageType.Weapon);
                    DestroyProjectile();
                }
                else if (collider.tag == GameInfo.TagProjectile || collider.tag == GameInfo.TagLazer)
                {
                    // supposed to make any colliding projectiles destroy each other on impact
                    Destroy(collider.gameObject);
                    DestroyProjectile();
                }

                void DestroyProjectile()
                {
                    this.alive = false;
                    Destroy(this.gameObject);
                }
            }
        }
    }
}
