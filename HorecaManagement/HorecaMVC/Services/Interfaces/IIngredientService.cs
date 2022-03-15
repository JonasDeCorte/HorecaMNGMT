using Horeca.Shared.Dtos.Ingredients;

namespace Horeca.MVC.Services.Interfaces
{
    public interface IIngredientService
    {
        public Task<IEnumerable<IngredientDto>> GetIngredients();
        public Task<IngredientDto> GetIngredientById(int id);
        public void AddIngredient(MutateIngredientDto ingredient);
        public void DeleteIngredient(int id);
        public void UpdateIngredient(MutateIngredientDto ingredient);
    }
}
