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
        public void AddMenu(MutateMenuDto menu);
        public void AddMenuDish(int id, MutateDishMenuDto dish);
        public void DeleteMenu(int id);
        public void DeleteMenuDish(DeleteDishMenuDto dish);
        public void UpdateMenu(MutateMenuDto menu);

    }
}
