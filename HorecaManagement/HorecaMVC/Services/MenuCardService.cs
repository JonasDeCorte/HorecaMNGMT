using Horeca.MVC.Helpers.Mappers;
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
        private readonly HttpClient httpClient;
        private readonly IConfiguration configuration;
        private readonly IRestaurantService restaurantService;

        public MenuCardService(HttpClient httpClient, IConfiguration configuration, IRestaurantService restaurantService)
        {
            this.httpClient = httpClient;
            this.configuration = configuration;
            this.restaurantService = restaurantService;
        }

        public async Task<IEnumerable<MenuCardDto>> GetMenuCards()
        {
            var response = await httpClient.GetAsync($"{configuration.GetSection("BaseURL").Value}/{ClassConstants.MenuCard}/" +
                $"{ClassConstants.Restaurant}/{restaurantService.GetCurrentRestaurantId()}");
            if (response.IsSuccessStatusCode)
            {
                var result = JsonConvert.DeserializeObject<IEnumerable<MenuCardDto>>(await response.Content.ReadAsStringAsync());
                if (result == null)
                {
                    return new List<MenuCardDto>();
                }
                return result;
            }
            return null;
        }

        public async Task<MenuCardDto> GetMenuCardById(int id)
        {
            var response = await httpClient.GetAsync($"{configuration.GetSection("BaseURL").Value}/{ClassConstants.MenuCard}/{id}/" +
                $"{ClassConstants.Restaurant}/{restaurantService.GetCurrentRestaurantId()}");
            if (response.IsSuccessStatusCode)
            {
                var result = JsonConvert.DeserializeObject<MenuCardDto>(await response.Content.ReadAsStringAsync());
                if (result == null)
                {
                    return new MenuCardDto();
                }
                return result;
            }
            return null;
        }

        public async Task<MenuCardsByIdDto> GetListsByMenuCardId(int id)
        {
            var response = await httpClient.GetAsync($"{configuration.GetSection("BaseURL").Value}/{ClassConstants.MenuCard}/{id}" +
                $"/{ClassConstants.Menus}/{ClassConstants.Dishes}/{ClassConstants.Restaurant}/{restaurantService.GetCurrentRestaurantId()}");
            if (response.IsSuccessStatusCode)
            {
                var result = JsonConvert.DeserializeObject<MenuCardsByIdDto>(await response.Content.ReadAsStringAsync());
                if (result == null)
                {
                    return new MenuCardsByIdDto();
                }
                return result;
            }
            return null;
        }

        public async Task<MenuCard> GetMenuCardDetailById(int id)
        {
            MenuCardDto menuCardDto = await GetMenuCardById(id);
            MenuCardsByIdDto menuCardListsDto = await GetListsByMenuCardId(id);
            if (menuCardDto == null || menuCardListsDto == null)
            {
                return null;
            }

            return MenuCardMapper.MapMenuCardDetail(menuCardDto, menuCardListsDto);
        }

        public async Task<HttpResponseMessage> AddMenuCard(MutateMenuCardDto menuCard)
        {
            var request = new HttpRequestMessage(HttpMethod.Post,
                $"{configuration.GetSection("BaseURL").Value}/{ClassConstants.MenuCard}/{ClassConstants.Restaurant}/" +
                $"{restaurantService.GetCurrentRestaurantId()}")
            {
                Content = new StringContent(JsonConvert.SerializeObject(menuCard), Encoding.UTF8, "application/json")
            };

            var response = await httpClient.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                return response;
            }
            return null;
        }

        public async Task<HttpResponseMessage> AddMenuCardDish(int id, MutateDishMenuCardDto dish)
        {
            var request = new HttpRequestMessage(HttpMethod.Post,
                $"{configuration.GetSection("BaseURL").Value}/{ClassConstants.MenuCard}/{id}/" +
                $"{ClassConstants.Dishes}/{ClassConstants.Restaurant}/{restaurantService.GetCurrentRestaurantId()}")
            {
                Content = new StringContent(JsonConvert.SerializeObject(dish), Encoding.UTF8, "application/json")
            };

            var response = await httpClient.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                return response;
            }
            return null;
        }

        public async Task<HttpResponseMessage> AddMenuCardMenu(int id, MutateMenuMenuCardDto menu)
        {
            var request = new HttpRequestMessage(HttpMethod.Post,
                $"{configuration.GetSection("BaseURL").Value}/{ClassConstants.MenuCard}/{id}/" +
                $"{ClassConstants.Menus}/{ClassConstants.Restaurant}/{restaurantService.GetCurrentRestaurantId()}")
            {
                Content = new StringContent(JsonConvert.SerializeObject(menu), Encoding.UTF8, "application/json")
            };

            var response = await httpClient.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                return response;
            }
            return null;
        }

        public async Task<HttpResponseMessage> DeleteMenuCard(int id)
        {
            var response = await httpClient.DeleteAsync($"{configuration.GetSection("BaseURL").Value}/{ClassConstants.MenuCard}/{id}/" +
                $"{ClassConstants.Restaurant}/{restaurantService.GetCurrentRestaurantId()}");
            if (response.IsSuccessStatusCode)
            {
                return response;
            }
            return null;
        }

        public async Task<HttpResponseMessage> DeleteMenuCardDish(DeleteDishMenuCardDto dish)
        {
            var request = new HttpRequestMessage(HttpMethod.Delete,
                $"{configuration.GetSection("BaseURL").Value}/{ClassConstants.MenuCard}/{dish.MenuCardId}/" +
                $"{ClassConstants.Dishes}/{dish.DishId}/{ClassConstants.Restaurant}/{restaurantService.GetCurrentRestaurantId()}")
            {
                Content = new StringContent(JsonConvert.SerializeObject(dish), Encoding.UTF8, "application/json")
            };

            var response = await httpClient.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                return response;
            }
            return null;
        }

        public async Task<HttpResponseMessage> DeleteMenuCardMenu(DeleteMenuMenuCardDto menu)
        {
            var request = new HttpRequestMessage(HttpMethod.Delete,
                $"{configuration.GetSection("BaseURL").Value}/{ClassConstants.MenuCard}/{menu.MenuCardId}/{ClassConstants.Menus}/" +
                $"{menu.MenuId}/{ClassConstants.Restaurant}/{restaurantService.GetCurrentRestaurantId()}")
            {
                Content = new StringContent(JsonConvert.SerializeObject(menu), Encoding.UTF8, "application/json")
            };

            var response = await httpClient.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                return response;
            }
            return null;
        }

        public async Task<HttpResponseMessage> UpdateMenuCard(MutateMenuCardDto menuCard)
        {
            var request = new HttpRequestMessage(HttpMethod.Put,
                $"{configuration.GetSection("BaseURL").Value}/{ClassConstants.MenuCard}/{ClassConstants.Restaurant}/" +
                $"{restaurantService.GetCurrentRestaurantId()}")
            {
                Content = new StringContent(JsonConvert.SerializeObject(menuCard), Encoding.UTF8, "application/json")
            };

            var response = await httpClient.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                return response;
            }
            return null;
        }

        public async Task<HttpResponseMessage> UpdateMenuCardDish(MutateDishMenuCardDto dishMenuCardDto)
        {
            var request = new HttpRequestMessage(HttpMethod.Put,
                $"{configuration.GetSection("BaseURL").Value}/{ClassConstants.MenuCard}/{dishMenuCardDto.MenuCardId}/" +
                $"{ClassConstants.Dishes}/{dishMenuCardDto.Dish.Id}/{ClassConstants.Restaurant}/{restaurantService.GetCurrentRestaurantId()}")
            {
                Content = new StringContent(JsonConvert.SerializeObject(dishMenuCardDto), Encoding.UTF8, "application/json")
            };

            var response = await httpClient.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                return response;
            }
            return null;
        }

        public async Task<HttpResponseMessage> UpdateMenuCardMenu(MutateMenuMenuCardDto menuMenuCardDto)
        {
            var request = new HttpRequestMessage(HttpMethod.Put,
                $"{configuration.GetSection("BaseURL").Value}/{ClassConstants.MenuCard}/{menuMenuCardDto.MenuCardId}/" +
                $"{ClassConstants.Menus}/{menuMenuCardDto.Menu.Id}/{ClassConstants.Restaurant}/{restaurantService.GetCurrentRestaurantId()}")
            {
                Content = new StringContent(JsonConvert.SerializeObject(menuMenuCardDto), Encoding.UTF8, "application/json")
            };

            var response = await httpClient.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                return response;
            }
            return null;
        }
    }
}