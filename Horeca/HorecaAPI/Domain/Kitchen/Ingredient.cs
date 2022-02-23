﻿namespace Domain.Kitchen
{
    public class Ingredient
    {
        public string Name { get; set; }
        public IngredientType IngredientType { get; set; }

        public Ingredient(string name, IngredientType ingredientType)
        {
            Name = name;
            IngredientType = ingredientType;
        }
    }
}
