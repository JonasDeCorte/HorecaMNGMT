namespace HorecaShared.Dishes
{
    public interface IDishService
    {
        Task<DishResponse.GetIndex> GetIndexAsync(DishRequest.GetIndex request);

        Task<DishResponse.GetDetail> GetDetailAsync(DishRequest.GetDetail request);

        Task DeleteAsync(DishRequest.Delete request);

        Task<DishResponse.Create> CreateAsync(DishRequest.Create request);

        Task<DishResponse.Edit> EditAsync(DishRequest.Edit request);
    }
}
