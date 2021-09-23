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

    public Image Image => image;

    public void UpdateText()
    {
        textAmount.text = Amount.ToString();
    }
}
