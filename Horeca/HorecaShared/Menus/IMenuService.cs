namespace HorecaShared.Menus
{
    public interface IMenuService
    {
        Task<MenuResponse.GetIndex> GetIndexAsync(MenuRequest.GetIndex request);

        Task<MenuResponse.GetDetail> GetDetailAsync(MenuRequest.GetDetail request);

        Task DeleteAsync(MenuRequest.Delete request);

        Task<MenuResponse.Create> CreateAsync(MenuRequest.Create request);

        Task<MenuResponse.Edit> EditAsync(MenuRequest.Edit request);
    }
}
