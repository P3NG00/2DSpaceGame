using SpaceGame;
using SpaceGame.Settings;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIInventorySlot : MonoBehaviour
{
    [Header("Info", order = 5)]
    public ItemInfo Item;
    public int Amount;

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
        this.textAmount.text = this.Amount.ToString();
    }

    public void SetVisible(bool visible)
    {
        this.image.enabled = visible;
        this.textAmount.enabled = visible;
    }

    public int AddAmount(int amount)
    {
        this.Amount += amount;
        int difference = 0;

        if (this.Amount > this.Item.MaxStackSize)
        {
            difference = this.Amount - this.Item.MaxStackSize;
            this.Amount = this.Item.MaxStackSize;
        }

        return difference;
    }

    public void SelectSlot()
    {
        GameInfo.SelectSlot(this);
    }
}
