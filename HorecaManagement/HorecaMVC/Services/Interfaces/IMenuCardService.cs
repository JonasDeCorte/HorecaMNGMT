using Horeca.Shared.Data.Entities;
using Horeca.Shared.Dtos.MenuCards;

namespace Horeca.MVC.Services.Interfaces
{
    public interface IMenuCardService
    {
        public Task<IEnumerable<MenuCardDto>> GetMenuCards();
        public Task<MenuCardDto> GetMenuCardById(int id);
        public Task<MenuCardsByIdDto> GetListsByMenuCardId(int id);
        public Task<MenuCard> GetMenuCardDetailById(int id);
        public void AddMenuCard(MutateMenuCardDto menuCard);
        public void AddMenuCardDish(int id, MutateDishMenuCardDto dish);
        public void AddMenuCardMenu(int id, MutateMenuMenuCardDto menu);
        public void DeleteMenuCard(int id);
        public void DeleteMenuCardDish(DeleteDishMenuCardDto dish);
        public void DeleteMenuCardMenu(DeleteMenuMenuCardDto menu);
        public void UpdateMenuCard(MutateMenuCardDto menuCard);
        public void UpdateMenuCardDish(MutateDishMenuCardDto menuCard);
        public void UpdateMenuCardMenu(MutateMenuMenuCardDto menuCard);
    }
}
