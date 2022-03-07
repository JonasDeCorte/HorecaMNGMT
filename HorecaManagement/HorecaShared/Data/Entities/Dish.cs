﻿namespace Horeca.Shared.Data.Entities
{
    public class Dish : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public string DishType { get; set; }
        public List<Ingredient> Ingredients { get; set; } = new();
    }
}