using SpaceGame.Ships;
using SpaceGame.SpaceObjects;
using UnityEngine;

namespace SpaceGame.Lazers
{
    public sealed class Lazer : MonoBehaviour
    {
        [Header("Info [Lazer]", order = 0)]
        [SerializeField] private LazerInfo lazerInfo;

        [Header("References [Lazer]", order = 100)]
        [SerializeField] private SpriteRenderer spriteRenderer;

        private Ship sourceShip;

        public static Lazer Create(LazerInfo lazerInfo, Ship source)
        {
            Vector2 offset = source.transform.up * (lazerInfo.Length / 2f);
            Lazer lazer = Instantiate(lazerInfo.LazerObject, source.Position + offset, source.transform.rotation, source.transform);
            lazer.transform.localScale = new Vector2(1f, lazerInfo.Length);
            lazer.spriteRenderer.color = lazerInfo.Item.Color;
            lazer.sourceShip = source;
            return lazer;
        }

        public LazerInfo LazerInfo => this.lazerInfo;
        public Ship SourceShip => this.sourceShip;

        private void OnTriggerStay2D(Collider2D collider)
        {
            if (collider.tag == GameInfo.TagShip || collider.tag == GameInfo.TagPlayer)
            {
                Ship ship = collider.GetComponent<Ship>();

                if (this.sourceShip != ship)
                {
                    ship.Damage(this.lazerInfo.Damage * Time.deltaTime, Enums.DamageType.Weapon, lazerInfo.Item.Effects);
                }
            }
            else if (collider.tag == GameInfo.TagSpaceRock)
            {
                SpaceObject spaceRock = collider.GetComponent<SpaceObject>();
                spaceRock.Damage(this.lazerInfo.Damage * Time.deltaTime, Enums.DamageType.Weapon);
            }
        }
    }
}
