using System.Collections;
using SpaceGame.Ships;
using SpaceGame.Utilities;
using UnityEngine;

namespace SpaceGame.Items
{
    [CreateAssetMenu(menuName = "2D Space Game/Item/Blink Jump", fileName = "Item Blink Jump")]
    public sealed class ItemInfoBlinkJump : ItemInfo
    {
        [Header("Info [ItemInfoBlinkJump]", order = 5)]
        // How far the blink jump should take you
        [SerializeField] private float jumpDistance;
        // How long to wait before re-appearing from the blink jump
        [SerializeField] private float jumpTime;

        public sealed override void Use(Ship source)
        {
            // TODO blink jump
            // start coroutine on ship to wait for re-appearance
            source.StartItemRoutine(this.RoutineBlinkJump(source));
            // TODO give time between each use
        }

        private IEnumerator RoutineBlinkJump(Ship ship)
        {
            // TODO needs testing
            // TODO REPLACE Util.ToggleActive(), CREATE FUNCTION IN SHIPS FOR INVISIBILITY/COLLISION
            Util.ToggleActive(ship.gameObject);
            yield return new WaitForSeconds(jumpTime);
            ship.Rigidbody.MovePosition(ship.transform.position + ship.transform.up * this.jumpDistance);
            Util.ToggleActive(ship.gameObject);
            ship.StopItemRoutine();
        }
    }
}