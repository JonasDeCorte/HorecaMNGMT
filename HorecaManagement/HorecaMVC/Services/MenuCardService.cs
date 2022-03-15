using Horeca.MVC.Services.Interfaces;
using Horeca.Shared.Constants;
using Horeca.Shared.Data.Entities;
using Horeca.Shared.Dtos.MenuCards;
using Newtonsoft.Json;
using System.Text;

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

        public async Task<IEnumerable<MenuCard>> GetMenuCards()
        {
            var response = await httpClient.GetAsync($"{configuration.GetSection("BaseURL").Value}/{ClassConstants.MenuCard}");

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            var result = JsonConvert.DeserializeObject<IEnumerable<MenuCard>>(response.Content.ReadAsStringAsync().Result);
            return result;
        }
        public async Task<MenuCard> GetMenuCardById(int id)
        {
            var response = await httpClient.GetAsync($"{configuration.GetSection("BaseURL").Value}/{ClassConstants.MenuCard}/{id}");

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            var result = JsonConvert.DeserializeObject<MenuCard>(response.Content.ReadAsStringAsync().Result);

            var listResult = await GetMenuCardListsById(id);
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

        public async Task<MenuCardsByIdDto> GetMenuCardListsById(int id)
        {
            var response = await httpClient.GetAsync($"{configuration.GetSection("BaseURL").Value}/{ClassConstants.MenuCard}/{id}" +
                $"/{ClassConstants.Menus}/{ClassConstants.Dishes}");

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            var result = JsonConvert.DeserializeObject<MenuCardsByIdDto>(response.Content.ReadAsStringAsync().Result);
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

        public void DeleteMenuCardDish(DeleteDishMenuCardDto dish)
        {
            var request = new HttpRequestMessage(HttpMethod.Delete,
                $"{configuration.GetSection("BaseURL").Value}/{ClassConstants.MenuCard}/{dish.MenuCardId}/{ClassConstants.Dishes}/" +
                $"{dish.DishId}");
            request.Content = new StringContent(JsonConvert.SerializeObject(dish), Encoding.UTF8, "application/json");
            httpClient.SendAsync(request);
        }

        public void DeleteMenuCardMenu(DeleteMenuMenuCardDto menu)
        {
            var request = new HttpRequestMessage(HttpMethod.Delete,
                $"{configuration.GetSection("BaseURL").Value}/{ClassConstants.MenuCard}/{menu.MenuCardId}/{ClassConstants.Menus}/" +
                $"{menu.MenuId}");
            request.Content = new StringContent(JsonConvert.SerializeObject(menu), Encoding.UTF8, "application/json");
            httpClient.SendAsync(request);
        }

        public void UpdateMenuCard(MenuCard menuCard)
        {
            httpClient.PutAsJsonAsync($"{configuration.GetSection("BaseURL").Value}/{ClassConstants.MenuCard}", menuCard);
        }
    }
}