using System.Collections.Concurrent;
using System.IO;
using SpaceGame;
using SpaceGame.Items;
using SpaceGame.Utilities;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SpaceGame.UI
{
    public sealed class UIInventorySlot : MonoBehaviour
    {
        [Header("Info", order = 5)]
        public ItemStack ItemStack;

        [Header("Linked Inventory Movement", order = 10)]
        [SerializeField] private UIInventorySlot slotUp;
        [SerializeField] private UIInventorySlot slotLeft;
        [SerializeField] private UIInventorySlot slotRight;
        [SerializeField] private UIInventorySlot slotDown;

        [Header("References", order = 99)]
        [SerializeField] private Image image;
        [SerializeField] private TMP_Text textAmount;
        [SerializeField] private Button button;
        [SerializeField] private RectTransform rectTransform;

        public Image Image => this.image;
        public Button Button => this.button;
        public RectTransform RectTransform => this.rectTransform;

        public void UpdateText()
        {
            this.textAmount.text = this.ItemStack.Amount.ToString();
        }

        public void SetVisible(bool visible)
        {
            this.image.enabled = visible;
            this.textAmount.enabled = this.ItemStack.Amount > 1;
        }

        public void NextSlot(Enums.Direction direction)
        {
            UIInventorySlot slot = null;

            switch (direction)
            {
                case Enums.Direction.Down: slot = this.slotDown; break;
                case Enums.Direction.Left: slot = this.slotLeft; break;
                case Enums.Direction.Right: slot = this.slotRight; break;
                case Enums.Direction.Up: slot = this.slotUp; break;
            }

            if (slot != null)
            {
                GameInfo.HoverSlot(slot);
            }
        }

        public void Select() => GameInfo.SelectSlot(this);
    }
}
