using Horeca.Shared.Data.Entities;

namespace HorecaMVC.Models.Ingredients
{
    public class IngredientViewModel
    {
        public string Name { get; set; }
        public string IngredientType { get; set; }
        public int BaseAmount { get; set; }
        public Unit Unit { get; set; }

        public IngredientViewModel(Ingredient ingredient)
        {
            Name = ingredient.Name;
            IngredientType = ingredient.IngredientType;
            BaseAmount = ingredient.BaseAmount;
            Unit = ingredient.Unit;
        }
    }
}
