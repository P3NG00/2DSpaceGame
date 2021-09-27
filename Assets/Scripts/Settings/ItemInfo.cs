using UnityEngine;

namespace SpaceGame.Settings
{
    [CreateAssetMenu(menuName = "2D Space Game/Item", fileName = "Item Info")]
    public class ItemInfo : ScriptableObject
    {
        [Header("Info", order = 0)]
        [SerializeField] private string itemName;
        [SerializeField] private Sprite sprite;
        [SerializeField] private Color color;
        // TODO implement max stack size (if given more than available in slot, move to next slot) (if no more slots available, leave on ground)

        public Sprite Sprite => sprite;
        public Color Color => color;
        public string Name => name;
    }
}
