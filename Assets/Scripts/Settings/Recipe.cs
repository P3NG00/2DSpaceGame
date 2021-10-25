using UnityEngine;

namespace SpaceGame.Items
{
    // TODO implement recipes

    [System.Serializable]
    public sealed class Recipe
    {
        [SerializeField] private ItemStack[] ingredients;
        [SerializeField] private ItemStack output;

        public ItemStack[] Ingreidents => this.ingredients;
        public ItemStack Output => this.output;
    }
}
