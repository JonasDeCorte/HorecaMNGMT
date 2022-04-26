using Horeca.Shared.Data.Entities;
using Horeca.Shared.Dtos.Menus;

namespace Horeca.MVC.Services.Interfaces
{
    public interface IMenuService
    {
        public Task<IEnumerable<MenuDto>> GetMenus();
        public Task<MenuDto> GetMenuById(int id);
        public Task<Menu> GetMenuDetailById(int id);
        public Task<MenuDishesByIdDto> GetDishesByMenuId(int id);
        public Task<HttpResponseMessage> AddMenu(MutateMenuDto menu);
        public Task<HttpResponseMessage> AddMenuDish(MutateDishMenuDto dish);
        public Task<HttpResponseMessage> DeleteMenu(int id);
        public Task<HttpResponseMessage> DeleteMenuDish(DeleteDishMenuDto dish);
        public Task<HttpResponseMessage> UpdateMenu(MutateMenuDto menu);
        public Task<HttpResponseMessage> UpdateMenuDish(MutateDishMenuDto dish);

    }
}
