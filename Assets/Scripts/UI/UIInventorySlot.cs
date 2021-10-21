using SpaceGame.Items;
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
        [SerializeField] private UIInventorySlot slotDown;
        [SerializeField] private UIInventorySlot slotLeft;
        [SerializeField] private UIInventorySlot slotRight;
        [SerializeField] private UIInventorySlot slotUp;

        [Header("References", order = 99)]
        [SerializeField] private Image image;
        [SerializeField] private TMP_Text textAmount;
        [SerializeField] private Button button;
        [SerializeField] private RectTransform rectTransform;

        public Image Image => this.image;
        public Button Button => this.button;
        public RectTransform RectTransform => this.rectTransform;

        public UIInventorySlot SlotDown => this.slotDown;
        public UIInventorySlot SlotLeft => this.slotLeft;
        public UIInventorySlot SlotRight => this.slotRight;
        public UIInventorySlot SlotUp => this.slotUp;

        public bool Visible { set { this.image.enabled = value; } }

        public void UpdateText()
        {
            int amount = this.ItemStack.Amount;
            this.textAmount.text = amount.ToString();
            this.textAmount.enabled = amount > 1;
        }

        public void Select() => GameInfo.SelectSlot(this);
    }
}
