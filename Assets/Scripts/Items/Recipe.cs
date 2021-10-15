using UnityEngine;

namespace SpaceGame.Items
{
    [CreateAssetMenu(menuName = "2D Space Game/Recipe", fileName = "Recipe")]
    public sealed class Recipe : ScriptableObject
    {
        [SerializeField] private ItemStack[] ingredients;
        [SerializeField] private ItemStack output;

        public ItemStack Output => this.output;

        public bool AreIngredientsValid(ItemStack[] input)
        {
            // TODO finish this function
            return false;
        }
    }
}
