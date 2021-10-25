using SpaceGame.Projectiles;
using SpaceGame.Ships;
using UnityEngine;

namespace SpaceGame.SpaceObjects
{
    // TODO delete this whole file, instead, use Projectile.cs
    public sealed class SpaceObjectProjectile : SpaceObject
    {
        [Header("DEBUG", order = 150)]
        public ProjectileInfo ProjectileInfo;
        public Ship SourceShip;
    }
}
