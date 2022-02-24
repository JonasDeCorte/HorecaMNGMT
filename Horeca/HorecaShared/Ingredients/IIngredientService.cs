namespace HorecaShared.Ingredients
{
    public interface IIngredientService
    {
        Task<IngredientResponse.GetIndex> GetIndexAsync(IngredientRequest.GetIndex request);

        Task<IngredientResponse.GetDetail> GetDetailAsync(IngredientRequest.GetDetail request);

        Task DeleteAsync(IngredientRequest.Delete request);

        Task<IngredientResponse.Create> CreateAsync(IngredientRequest.Create request);

        Task<IngredientResponse.Edit> EditAsync(IngredientRequest.Edit request);
    }
}