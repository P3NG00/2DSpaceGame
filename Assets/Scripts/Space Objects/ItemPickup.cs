using UnityEngine;

namespace SpaceGame.SpaceObjects
{
    /*
    ItemPickup is kept seperate so that Player can trigger pickup without colliding,
    while also allowing other SpaceObjects to collider with it
    */

    public sealed class ItemPickup : MonoBehaviour
    {
        [SerializeField] private ItemObject parentItem;

        private void OnTriggerEnter2D(Collider2D collider)
        {
            // If player triggered and item declared...
            if (collider.tag == GameInfo.TagPlayer & this.parentItem.Item != null)
            {
                int remaining = GameInfo.GiveItem(this.parentItem.Item, this.parentItem.Amount);

                if (remaining == 0)
                {
                    GameInfo.DestroySpaceObject(parentItem);
                }
                else
                {
                    this.parentItem.Amount = remaining;
                }
            }
        }
    }
}
