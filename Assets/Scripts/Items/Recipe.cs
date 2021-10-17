using System;
using System.Collections.Generic;
using SpaceGame.Settings;
using UnityEngine;

namespace SpaceGame.Items
{
    [System.Serializable]
    public sealed class Recipe
    {
        [SerializeField] private ItemStack[] ingredients;
        [SerializeField] private ItemStack output;

        public ItemStack[] Ingreidents => this.ingredients;
        public ItemStack Output => this.output;

        // TODO finish this function
        public bool AreIngredientsValid(ItemStack[] input)
        {
            // Add all items to dictionary for easy usage...
            Dictionary<Item, int> inputDict = new Dictionary<Item, int>();

            foreach (ItemStack itemStack in input)
            {
                if (inputDict.ContainsKey(itemStack.Item))
                {
                    inputDict[itemStack.Item] += itemStack.Amount;
                }
                else
                {
                    inputDict.Add(itemStack.Item, itemStack.Amount);
                }
            }

            // Find if ingredients match...
            bool hasAll = true;

            foreach (ItemStack itemStack in this.ingredients)
            {
                if (inputDict.ContainsKey(itemStack.Item))
                {
                    if (inputDict[itemStack.Item] < itemStack.Amount)
                    {
                        hasAll = false;
                        break;
                    }
                }
                else
                {
                    hasAll = false;
                    break;
                }
            }

            return hasAll;
        }
    }
}
