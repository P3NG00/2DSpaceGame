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

    public Image Image => image;
    public Button Button => button;
    public RectTransform RectTransform => rectTransform;

    public void UpdateText()
    {
        textAmount.text = Amount.ToString();
    }

    public void SetVisible(bool visible)
    {
        image.enabled = visible;
        textAmount.enabled = visible;
    }

    public void SelectSlot()
    {
        GameInfo.SelectSlot(this);
    }
}
