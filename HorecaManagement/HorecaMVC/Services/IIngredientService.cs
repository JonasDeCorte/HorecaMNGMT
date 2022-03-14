using Horeca.Shared.Data.Entities;

namespace Horeca.MVC.Services
{
    public interface IIngredientService
    {
        public Task<IEnumerable<Ingredient>> GetIngredients();
        public Task<Ingredient> GetIngredientById(int id);
        public void AddIngredient(Ingredient ingredient);
        public void DeleteIngredient(int id);
        public void UpdateIngredient(Ingredient ingredient);
    }
}
