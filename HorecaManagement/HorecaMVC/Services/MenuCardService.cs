using Horeca.MVC.Models.Mappers;
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
            var menu = httpClient.GetAsync($"{configuration.GetSection("BaseURL").Value}/{ClassConstants.MenuCard}/{id}");
            var result = JsonConvert.DeserializeObject<MenuCard>(menu.Result.Content.ReadAsStringAsync().Result);

            var listResult = GetMenuCardListsById(id);
            var menus = listResult.Menus.Select(x => new Menu
            {
                Id = x.Id,
                Name = x.Name,
                Category = x.Category,
                Description = x.Description
            });
            var dishes = listResult.Dishes.Select(x => new Dish
            {
                Id = x.Id,
                Name = x.Name,
                DishType = x.DishType,
                Category = x.Category
            });

            result.Menus.AddRange(menus);
            result.Dishes.AddRange(dishes);

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

        public void AddMenuCardDish(int id, MutateDishMenuCardDto dish)
        {
            httpClient.PostAsJsonAsync($"{configuration.GetSection("BaseURL").Value}/{ClassConstants.MenuCard}/{id}/" +
                $"{ClassConstants.Dishes}", dish);
        }

        public void AddMenuCardMenu(int id, MutateMenuMenuCardDto menu)
        {
            httpClient.PostAsJsonAsync($"{configuration.GetSection("BaseURL").Value}/{ClassConstants.MenuCard}/{id}/" +
                $"{ClassConstants.Menus}", menu);
        }

        public void DeleteMenuCard(int id)
        {
            httpClient.DeleteAsync($"{configuration.GetSection("BaseURL").Value}/{ClassConstants.MenuCard}/{id}");
        }

        public void UpdateMenuCard(MenuCard menuCard)
        {
            httpClient.PutAsJsonAsync($"{configuration.GetSection("BaseURL").Value}/{ClassConstants.MenuCard}", menuCard);
        }
    }
}