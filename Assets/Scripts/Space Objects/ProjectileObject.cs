using System.ComponentModel;
using UnityEngine;

namespace SpaceGame.SpaceObjects
{
    public sealed class ProjectileObject : SpaceObject
    {
        [Header("Info", order = 0)]
        [SerializeField] private int damage; // TODO replace with Weapon or Projectile Info
    }
}
