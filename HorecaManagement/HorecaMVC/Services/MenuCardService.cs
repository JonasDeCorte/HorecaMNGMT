using Horeca.Shared.Constants;
using Horeca.Shared.Data.Entities;
using Horeca.Shared.Dtos.MenuCards;
using Newtonsoft.Json;

namespace Horeca.MVC.Services
{
    public class MenuCardService : IMenuCardService
    {
        private HttpClient httpClient;
        private IConfiguration configuration;

        public MenuCardService(HttpClient httpClient, IConfiguration iConfig)
        {
            this.httpClient = httpClient;
            configuration = iConfig;
        }

        public IEnumerable<MenuCard> GetMenuCards()
        {
            var menuCards = httpClient.GetAsync($"{configuration.GetSection("BaseURL").Value}/{ClassConstants.MenuCard}");
            var result = JsonConvert.DeserializeObject<IEnumerable<MenuCard>>(menuCards.Result.Content.ReadAsStringAsync().Result);
            return result;
        }
        public MenuCard GetMenuCardById(int id)
        {
            var menu = httpClient.GetAsync($"{configuration.GetSection("BaseURL").Value}/{ClassConstants.Menu}/{id}");
            var result = JsonConvert.DeserializeObject<MenuCard>(menu.Result.Content.ReadAsStringAsync().Result);
            var listResult = GetMenuCardListsById(id);

            return result;
        }

        public MenuCardsByIdDto GetMenuCardListsById(int id)
        {
            var menuCardList = httpClient.GetAsync($"{configuration.GetSection("BaseURL").Value}/{ClassConstants.MenuCard}/{id}" +
                $"/{ClassConstants.Menus}/{ClassConstants.Dishes}");
            var result = JsonConvert.DeserializeObject<MenuCardsByIdDto>(menuCardList.Result.Content.ReadAsStringAsync().Result);

            return result;
        }

        public void AddMenuCard(MenuCard menuCard)
        {
            httpClient.PostAsJsonAsync($"{configuration.GetSection("BaseURL").Value}/{ClassConstants.MenuCard}", menuCard);
        }

        public void DeleteMenuCard(int id)
        {
            throw new NotImplementedException();
        }

        public void UpdateMenuCard(MenuCard menuCard)
        {
            throw new NotImplementedException();
        }
    }
}