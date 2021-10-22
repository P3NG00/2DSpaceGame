using SpaceGame.Settings;
using SpaceGame.SpaceObjects;
using UnityEngine;

namespace SpaceGame.Items
{
    public sealed class ItemProjectile : Item
    {
        [Header("Info [ItemProjectile]", order = 5)]
        [SerializeField] private ProjectileObject projectile;
        // TODO make rock chunks an ItemProjectile for testing
    }
}
