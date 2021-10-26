using SpaceGame.Ships;
using UnityEngine;

namespace SpaceGame.Projectiles
{
    public sealed class Projectile : MonoBehaviour
    {
        [Header("References [Projectile]", order = 95)]
        [SerializeField] private new Rigidbody2D rigidbody;
        [SerializeField] private SpriteRenderer spriteRenderer;

        [Header("DEBUG", order = 100)]
        [SerializeField] private ProjectileInfo projectileInfo;
        [SerializeField] private Ship sourceShip;

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
            // TODO
            // dont let projectile hit source ship
            // projectile should damage ships for ship damage amount
            // projectile should damage space objects for space object amount

            // TODO add more cases
            switch (collider.tag)
            {
                case "Player": /* TODO */ break;
            }
        }
    }
}