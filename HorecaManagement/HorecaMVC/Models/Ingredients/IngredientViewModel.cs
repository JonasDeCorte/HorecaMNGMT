using Horeca.Shared.Data.Entities;

namespace HorecaMVC.Models.Ingredients
{
    public class IngredientViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string IngredientType { get; set; }
        public int BaseAmount { get; set; }
        public Unit Unit { get; set; }
    }
}
