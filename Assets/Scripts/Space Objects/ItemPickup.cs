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
            if (collider.tag == GameInfo.TagPlayer & parentItem.Item != null)
            {
                int remaining = GameInfo.GiveItem(parentItem.Item, parentItem.Amount);

                if (remaining == 0)
                {
                    GameInfo.DestroySpaceObject(parentItem);
                }
                else
                {
                    parentItem.Amount = remaining;
                }
            }
        }
    }
}
