using SpaceGame.Items;
using UnityEngine;
using UnityEngine.UI;

namespace SpaceGame.UI
{
    public sealed class UICheatSlot : MonoBehaviour
    {
        [Header("References", order = 99)]
        [SerializeField] private Image image;
        [SerializeField] private RectTransform rectTransform;

        public RectTransform RectTransform => this.rectTransform;

        private ItemInfo itemInfo;

        public void SetItemInfo(ItemInfo itemInfo)
        {
            this.itemInfo = itemInfo;
            this.image.sprite = itemInfo.Sprite;
            this.image.color = itemInfo.Color;
        }

        public void CallbackButton_GiveItem() => GameInfo.GiveItem(new ItemStack(itemInfo));
    }
}
