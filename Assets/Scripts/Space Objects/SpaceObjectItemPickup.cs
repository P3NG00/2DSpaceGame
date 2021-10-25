using UnityEngine;

namespace SpaceGame.SpaceObjects
{
    // SpaceObjectItemPickup is  so that Player can trigger pickup without
    // colliding, while also allowing other SpaceObjects to collider with it

    // SpaceObjectItemPickup uses the SpaceObjectItem as its parent.
    // This class is kept seperate because for the SpaceObjectItem to
    // collide, a collider must still collide with other SpaceObjects,
    // but must have another collider for the player to trigger.

    public sealed class SpaceObjectItemPickup : MonoBehaviour
    {
        [SerializeField] private SpaceObjectItem parentItem;

        private void OnTriggerEnter2D(Collider2D collider)
        {
            // If player triggered and item declared...
            if (collider.tag == GameInfo.TagPlayer & this.parentItem.ItemStack != null)
            {
                int remaining = GameInfo.GiveItem(this.parentItem.ItemStack);

                if (remaining == 0)
                {
                    GameInfo.DestroySpaceObject(parentItem);
                }
                else
                {
                    this.parentItem.ItemStack.Amount = remaining;
                }
            }
        }
    }
}
