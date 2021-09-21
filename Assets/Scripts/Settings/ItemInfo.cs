using UnityEngine;

namespace SpaceGame.Settings
{
    [CreateAssetMenu(menuName = "2D Space Game/Settings/Item", fileName = "Item Info")]
    public class ItemInfo : ScriptableObject
    {
        [Header("Info", order = 0)]
        [SerializeField] private string itemName;
        [SerializeField] private Sprite sprite;
        [SerializeField] private Color color;

        public Sprite Sprite => sprite;
        public Color Color => color;
        public string Name => name;
    }
}