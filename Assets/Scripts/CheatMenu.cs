using SpaceGame.Items;
using SpaceGame.UI;
using UnityEngine;

namespace SpaceGame
{
    public sealed class CheatMenu : MonoBehaviour
    {
        [Header("Info", order = 0)]
        [SerializeField, Min(0)] private int uiArrayWidth = 5;
        [SerializeField, Min(0f)] private float uiSlotSize = 90f;
        [SerializeField] private float yStart;
        [SerializeField] private ItemInfo[] cheatMenuItems;

        [Header("References", order = 100)]
        [SerializeField] private UICheatSlot cheatSlotPrefab;

        // Unity Start method
        private void Start()
        {
            UICheatSlot slot;
            float x, y;

            for (int i = 0; i < cheatMenuItems.Length; i++)
            {
                slot = Instantiate(this.cheatSlotPrefab, Vector2.zero, Quaternion.identity, GameInfo.ParentCheatMenu.transform);
                slot.SetItemInfo(this.cheatMenuItems[i]);
                x = ((-(this.uiArrayWidth - 1) / 2f) + (i % 5)) * this.uiSlotSize;
                y = this.yStart - ((i / this.uiArrayWidth) * this.uiSlotSize);
                slot.RectTransform.localPosition = new Vector2(x, y);
            }
        }
    }
}
