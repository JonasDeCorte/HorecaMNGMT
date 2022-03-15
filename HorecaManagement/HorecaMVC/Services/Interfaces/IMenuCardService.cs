using Horeca.Shared.Data.Entities;
using Horeca.Shared.Dtos.MenuCards;

namespace Horeca.MVC.Services.Interfaces
{
    public interface IMenuCardService
    {
        public Task<IEnumerable<MenuCard>> GetMenuCards();
        public Task<MenuCard> GetMenuCardById(int id);
        public Task<MenuCardsByIdDto> GetMenuCardListsById(int id);
        public void AddMenuCard(MenuCard menuCard);
        public void AddMenuCardDish(int id, MutateDishMenuCardDto dish);
        public void AddMenuCardMenu(int id, MutateMenuMenuCardDto menu);
        public void DeleteMenuCard(int id);
        public void DeleteMenuCardDish(DeleteDishMenuCardDto dish);
        public void DeleteMenuCardMenu(DeleteMenuMenuCardDto menu);
        public void UpdateMenuCard(MenuCard menuCard);
    }
}
