using SpaceGame.Items;
using SpaceGame.SpaceObjects;
using SpaceGame.UI;
using UnityEngine;

namespace SpaceGame
{
    public sealed class CheatMenu : MonoBehaviour
    {
        [Header("Info", order = 0)]
        [SerializeField, Min(0f)] private float uiSlotSize = 90f;

        [Header("Info (Items)", order = 1)]
        [SerializeField, Min(0)] private int uiArrayWidth = 5;
        [SerializeField] private float yStart;
        [SerializeField] private ItemInfo[] cheatMenuItems;

        [Header("Info (Spawnables)", order = 2)]
        [SerializeField] private Vector2 startPos;
        [SerializeField] private SpaceObjectSpawnableInfo[] spawnables;

        [Header("References", order = 100)]
        [SerializeField] private UICheatSlot cheatSlotPrefab;
        [SerializeField] private UISpawnSlot spawnSlotPrefab;

        // Unity Start method
        private void Start()
        {
            // Items
            UICheatSlot slot;
            float x, y;
            int i;

            for (i = 0; i < cheatMenuItems.Length; i++)
            {
                slot = Instantiate(this.cheatSlotPrefab, Vector2.zero, Quaternion.identity, GameInfo.ParentCheatMenu.transform);
                slot.SetItemInfo(this.cheatMenuItems[i]);
                x = ((-(this.uiArrayWidth - 1) / 2f) + (i % 5)) * this.uiSlotSize;
                y = this.yStart - ((i / this.uiArrayWidth) * this.uiSlotSize);
                slot.RectTransform.localPosition = new Vector2(x, y);
            }

            // Spawnables
            UISpawnSlot spawnSlot;
            Vector2 position = this.startPos;

            for (i = 0; i < spawnables.Length; i++)
            {
                spawnSlot = Instantiate(this.spawnSlotPrefab, position, Quaternion.identity, GameInfo.ParentCheatMenu.transform);
                spawnSlot.SetSpawnable(this.spawnables[i]);
                spawnSlot.RectTransform.localPosition = position;
                position -= new Vector2(0f, this.uiSlotSize);
            }
        }
    }
}
