using Horeca.MVC.Helpers.Mappers;
using Horeca.MVC.Services.Interfaces;
using Horeca.Shared.Constants;
using Horeca.Shared.Data.Entities;
using Horeca.Shared.Dtos.Menus;
using Newtonsoft.Json;
using System.Text;

namespace Horeca.MVC.Services
{
    public class MenuService : IMenuService
    {
        private readonly HttpClient httpClient;
        private readonly IConfiguration configuration;
        private readonly IRestaurantService restaurantService;

        public MenuService(HttpClient httpClient, IConfiguration IConfig, IRestaurantService restaurantService)
        {
            this.httpClient = httpClient;
            configuration = IConfig;
            this.restaurantService = restaurantService;
        }

        public async Task<IEnumerable<MenuDto>> GetMenus()
        {
            var response = await httpClient.GetAsync($"{configuration.GetSection("BaseURL").Value}/{ClassConstants.Menu}/" +
                $"{ClassConstants.Restaurant}/{restaurantService.GetCurrentRestaurantId()}");

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            var result = JsonConvert.DeserializeObject<IEnumerable<MenuDto>>(await response.Content.ReadAsStringAsync());
            return result;
        }

        public async Task<MenuDto> GetMenuById(int id)
        {
            var response = await httpClient.GetAsync($"{configuration.GetSection("BaseURL").Value}/{ClassConstants.Menu}/{id}/" +
                $"{ClassConstants.Restaurant}/{restaurantService.GetCurrentRestaurantId()}");
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }
            var result = JsonConvert.DeserializeObject<MenuDto>(await response.Content.ReadAsStringAsync());

            return result;
        }

        public async Task<MenuDishesByIdDto> GetDishesByMenuId(int id)
        {
            var response = await httpClient.GetAsync($"{configuration.GetSection("BaseURL").Value}/{ClassConstants.Menu}/{id}/" +
                $"{ClassConstants.Dishes}/{ClassConstants.Restaurant}/{restaurantService.GetCurrentRestaurantId()}");
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }
            var listResult = JsonConvert.DeserializeObject<MenuDishesByIdDto>(await response.Content.ReadAsStringAsync());

            return listResult;
        }

        public async Task<Menu> GetMenuDetailById(int id)
        {
            var menuDto = await GetMenuById(id);
            var dishListDto = await GetDishesByMenuId(id);
            if (menuDto == null || dishListDto == null)
            {
                return null;
            }

            return MenuMapper.MapMenuDetail(menuDto, dishListDto);
        }

        public async Task<HttpResponseMessage> AddMenu(MutateMenuDto menu)
        {
            var request = new HttpRequestMessage(HttpMethod.Post,
                $"{configuration.GetSection("BaseURL").Value}/{ClassConstants.Menu}/{ClassConstants.Restaurant}/" +
                $"{restaurantService.GetCurrentRestaurantId()}")
            {
                Content = new StringContent(JsonConvert.SerializeObject(menu), Encoding.UTF8, "application/json")
            };
            var response = await httpClient.SendAsync(request);
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }
            return response;
        }

        public async Task<HttpResponseMessage> AddMenuDish(int id, MutateDishMenuDto dish)
        {
            var request = new HttpRequestMessage(HttpMethod.Post,
                $"{configuration.GetSection("BaseURL").Value}/{ClassConstants.Menu}/{id}/{ClassConstants.Dishes}/" +
                $"{ClassConstants.Restaurant}/{restaurantService.GetCurrentRestaurantId()}")
            {
                Content = new StringContent(JsonConvert.SerializeObject(dish), Encoding.UTF8, "application/json")
            };
            var response = await httpClient.SendAsync(request);
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }
            return response;
        }

        public async Task<HttpResponseMessage> DeleteMenu(int id)
        {
            var response = await httpClient.DeleteAsync($"{configuration.GetSection("BaseURL").Value}/{ClassConstants.Menu}/{id}/" +
                $"{ClassConstants.Restaurant}/{restaurantService.GetCurrentRestaurantId()}");
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }
            return response;
        }

        public async Task<HttpResponseMessage> DeleteMenuDish(DeleteDishMenuDto dish)
        {
            var request = new HttpRequestMessage(HttpMethod.Delete,
                $"{configuration.GetSection("BaseURL").Value}/{ClassConstants.Menu}/{dish.MenuId}/{ClassConstants.Dishes}/{dish.DishId}/" +
                $"{ClassConstants.Restaurant}/{restaurantService.GetCurrentRestaurantId()}")
            {
                Content = new StringContent(JsonConvert.SerializeObject(dish), Encoding.UTF8, "application/json")
            };

            var response = await httpClient.SendAsync(request);
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }
            return response;
        }

        public async Task<HttpResponseMessage> UpdateMenu(MutateMenuDto menu)
        {
            var request = new HttpRequestMessage(HttpMethod.Put,
                $"{configuration.GetSection("BaseURL").Value}/{ClassConstants.Menu}/{ClassConstants.Restaurant}/" +
                $"{restaurantService.GetCurrentRestaurantId()}")
            {
                Content = new StringContent(JsonConvert.SerializeObject(menu), Encoding.UTF8, "application/json")
            };

            var response = await httpClient.SendAsync(request);
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }
            return response;
        }

        public async Task<HttpResponseMessage> UpdateMenuDish(MutateDishMenuDto dishMenuDto)
        {
            var request = new HttpRequestMessage(HttpMethod.Put,
                $"{configuration.GetSection("BaseURL").Value}/{ClassConstants.Menu}/{dishMenuDto.Id}/" +
                $"{ClassConstants.Dishes}/{dishMenuDto.Dish.Id}/{ClassConstants.Restaurant}/{restaurantService.GetCurrentRestaurantId()}")
            {
                Content = new StringContent(JsonConvert.SerializeObject(dishMenuDto), Encoding.UTF8, "application/json")
            };

            var response = await httpClient.SendAsync(request);
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }
            return response;
        }
    }
}