using Horeca.Shared.Data.Entities;

namespace Horeca.MVC.Services
{
    public interface IMenuCardService
    {
        public IEnumerable<MenuCard> GetMenuCards();
        public MenuCard GetMenuCardById(int id);
        public void AddMenuCard(MenuCard menuCard);
        public void DeleteMenuCard(int id);
        public void UpdateMenuCard(MenuCard menuCard);
    }
}
