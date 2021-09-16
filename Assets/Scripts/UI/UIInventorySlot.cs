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

    public Color Color { set { image.color = value; } }

    private int index;

    private void Start()
    {
        index = Random.Range(0, GameInfo.Sprites.Length);
    }

    public void ChangeSprite()
    {
        Sprite[] sprites = GameInfo.Sprites;

        if (Random.value < 0.5f)
        {
            --index;

            if (index == -1)
            {
                index = sprites.Length - 1;
            }
        }
        else
        {
            ++index;

            if (index == sprites.Length)
            {
                index = 0;
            }
        }

        image.sprite = sprites[index];

        textAmount.text = Amount.ToString();
    }
}
