using Horeca.Shared.Data.Entities;
using Horeca.Shared.Dtos.MenuCards;

namespace Horeca.MVC.Services
{
    public interface IMenuCardService
    {
        public IEnumerable<MenuCard> GetMenuCards();
        public MenuCard GetMenuCardById(int id);
        public void AddMenuCard(MenuCard menuCard);
        public void AddMenuCardDish(int id, MutateDishMenuCardDto dish);
        public void AddMenuCardMenu(int id, MutateMenuMenuCardDto menu);
        public void DeleteMenuCard(int id);
        public void DeleteMenuCardDish(DeleteDishMenuCardDto dish);
        public void DeleteMenuCardMenu(DeleteMenuMenuCardDto menu);
        public void UpdateMenuCard(MenuCard menuCard);
    }
}
