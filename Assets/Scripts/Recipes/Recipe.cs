using System.Collections.Generic;
using SpaceGame.Items;
using SpaceGame.UI;
using UnityEngine;

namespace SpaceGame.Recipes
{
    // TODO create RecipeList class
    // functions:
    // Recipe[] GetValidRecipes(UIInventorySlot inventory)

    [System.Serializable]
    public class Recipe
    {
        [SerializeField] private ItemStack[] ingredients;
        [SerializeField] private ItemStack result;

        // TODO test
        public bool Craft(UIInventorySlot[] inventory, bool consumeIngredients = true)
        {
            ItemStack ingredient;
            int ingredientAmount;
            List<List<ItemStack>> allLocations = new List<List<ItemStack>>();
            List<ItemStack> ingredientLocations = new List<ItemStack>();
            int indexIngredient, indexInventory;

            // Check each ingrdient...
            for (indexIngredient = 0; indexIngredient < this.ingredients.Length; indexIngredient++)
            {
                ingredient = this.ingredients[indexIngredient];
                ingredientAmount = 0;

                if (consumeIngredients)
                {
                    ingredientLocations.Clear();
                }

                // Check inventory for ingredient...
                for (indexInventory = 0; indexInventory < inventory.Length; indexInventory++)
                {
                    // If not enough ingredients continue looking...
                    if (ingredientAmount < ingredient.Amount)
                    {
                        ItemStack slotStack = inventory[indexInventory].ItemStack;

                        if (slotStack.ItemInfo == ingredient.ItemInfo)
                        {
                            ingredientAmount += slotStack.Amount;

                            if (consumeIngredients)
                            {
                                ingredientLocations.Add(slotStack);
                            }
                        }
                    }
                    // If enough ingredients break out of loop...
                    else
                    {
                        break;
                    }
                }

                // If not enough of ingredient...
                if (ingredientAmount < ingredient.Amount)
                {
                    return false;
                }
                else if (consumeIngredients)
                {
                    allLocations.Add(ingredientLocations);
                }
            }

            if (consumeIngredients)
            {
                // Remove all ingredients from inventory...
                for (indexIngredient = 0; indexIngredient < this.ingredients.Length; indexIngredient++)
                {
                    ingredientAmount = this.ingredients[indexIngredient].Amount;
                    ingredientLocations = allLocations[indexIngredient];

                    // Remove ingredients from cached slots...
                    foreach (ItemStack ingredientLocation in ingredientLocations)
                    {
                        ingredientAmount = ingredientLocation.ModifyAmount(-ingredientAmount);
                    }
                }
            }

            return true;
        }
    }
}
