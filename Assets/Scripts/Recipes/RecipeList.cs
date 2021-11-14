using SpaceGame.UI;
using UnityEngine;
using System.Linq;

namespace SpaceGame.Recipes
{
    public class RecipeList : MonoBehaviour
    {
        [Header("Info [RecipeList]", order = 0)]
        [SerializeField] private Recipe[] recipes;

        // Returns recipes that are currently craftable
        public Recipe[] GetValidRecipes(UIInventorySlot[] inventory) => (from Recipe recipe in recipes where recipe.Craft(inventory, false) select recipe).ToArray();
    }
}
