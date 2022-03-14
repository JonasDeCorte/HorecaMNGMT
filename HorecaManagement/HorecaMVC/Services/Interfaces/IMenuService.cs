using Horeca.Shared.Data.Entities;
using Horeca.Shared.Dtos.Menus;

namespace Horeca.MVC.Services.Interfaces
{
    public interface IMenuService
    {
        public Task<IEnumerable<Menu>> GetMenus();
        public Task<Menu> GetMenuById(int id);
        public Task<MenuDishesByIdDto> GetMenuDishesById(int id);
        public void AddMenu(Menu menu);
        public void AddMenuDish(int id, MutateDishMenuDto dish);
        public void DeleteMenu(int id);
        public void DeleteMenuDish(DeleteDishMenuDto dish);
        public void UpdateMenu(Menu menu);

    }
}
