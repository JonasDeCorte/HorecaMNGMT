using Horeca.Shared.Data.Entities;

namespace Horeca.MVC.Services
{
    public interface IIngredientService
    {
        public IEnumerable<Ingredient> GetIngredients();
        public Ingredient GetIngredientById(int id);
        public void AddIngredient(Ingredient ingredient);
        public void DeleteIngredient(int id);
        public void UpdateIngredient(Ingredient ingredient);
    }
}
