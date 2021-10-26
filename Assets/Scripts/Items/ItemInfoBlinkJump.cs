using SpaceGame.Ships;
using UnityEngine;

namespace SpaceGame.Items
{
    public sealed class ItemInfoBlinkJump : ItemInfo
    {
        [Header("Info [ItemInfoBlinkJump]", order = 5)]
        // How far the blink jump should take you
        [SerializeField] private float jumpDistane;
        // How long to wait before re-appearing from the blink jump
        [SerializeField] private float jumpTime;

        public sealed override void Use(Ship source)
        {
            // TODO blink jump
            // start coroutine on ship to wait for re-appearance
        }
    }
}