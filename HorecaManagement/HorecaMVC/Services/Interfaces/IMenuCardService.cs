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
        public Task<HttpResponseMessage> AddMenuCard(MutateMenuCardDto menuCard);
        public Task<HttpResponseMessage> AddMenuCardDish(int id, MutateDishMenuCardDto dish);
        public Task<HttpResponseMessage> AddMenuCardMenu(int id, MutateMenuMenuCardDto menu);
        public Task<HttpResponseMessage> DeleteMenuCard(int id);
        public Task<HttpResponseMessage> DeleteMenuCardDish(DeleteDishMenuCardDto dish);
        public Task<HttpResponseMessage> DeleteMenuCardMenu(DeleteMenuMenuCardDto menu);
        public Task<HttpResponseMessage> UpdateMenuCard(MutateMenuCardDto menuCard);
        public Task<HttpResponseMessage> UpdateMenuCardDish(MutateDishMenuCardDto menuCard);
        public Task<HttpResponseMessage> UpdateMenuCardMenu(MutateMenuMenuCardDto menuCard);
    }
}
