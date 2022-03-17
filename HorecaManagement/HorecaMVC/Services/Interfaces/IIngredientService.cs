using Horeca.Shared.Dtos.Ingredients;

namespace Horeca.MVC.Services.Interfaces
{
    public interface IIngredientService
    {
        public Task<IEnumerable<IngredientDto>> GetIngredients();
        public Task<IngredientDto> GetIngredientById(int id);
        public Task<HttpResponseMessage> AddIngredient(MutateIngredientDto ingredient);
        public Task<HttpResponseMessage> DeleteIngredient(int id);
        public Task<HttpResponseMessage> UpdateIngredient(MutateIngredientDto ingredient);
    }
}
