using Horeca.Shared.Constants;
using Horeca.Shared.Data.Entities;
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
            throw new NotImplementedException();
        }

        public void AddMenuCard(MenuCard menuCard)
        {
            throw new NotImplementedException();
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