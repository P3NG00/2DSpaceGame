using SpaceGame;
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

        public void SelectSlot()
        {
            GameInfo.SelectSlot(this);
        }
    }
}
