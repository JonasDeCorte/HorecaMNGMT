using Horeca.Shared.Data.Entities;

namespace Horeca.MVC.Services
{
    public interface IMenuService
    {
        public IEnumerable<Menu> GetMenus();
        public Menu GetMenuById(int id);
        public void AddMenu(Menu menu);
        public void DeleteMenu(int id);
        public void UpdateMenu(Menu menu);

    }
}
