using Horeca.MVC.Models.Mappers;
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
        private HttpClient httpClient;
        private IConfiguration configuration;

        public MenuService(HttpClient httpClient, IConfiguration IConfig)
        {
            this.httpClient = httpClient;
            configuration = IConfig;
        }

        public async Task<IEnumerable<MenuDto>> GetMenus()
        {
            var response = await httpClient.GetAsync($"{configuration.GetSection("BaseURL").Value}/{ClassConstants.Menu}");

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            var result = JsonConvert.DeserializeObject<IEnumerable<MenuDto>>(response.Content.ReadAsStringAsync().Result);
            return result;
        }

        public async Task<MenuDto> GetMenuById(int id)
        {
            var response = await httpClient.GetAsync($"{configuration.GetSection("BaseURL").Value}/{ClassConstants.Menu}/{id}");
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }
            var result = JsonConvert.DeserializeObject<MenuDto>(response.Content.ReadAsStringAsync().Result);

            return result;
        }

        public async Task<MenuDishesByIdDto> GetDishesByMenuId(int id)
        {
            var response = await httpClient.GetAsync($"{configuration.GetSection("BaseURL").Value}/{ClassConstants.Menu}/{id}/" +
                $"{ClassConstants.Dishes}");
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }
            var listResult = JsonConvert.DeserializeObject<MenuDishesByIdDto>(response.Content.ReadAsStringAsync().Result);

            return listResult;
        }

        public async Task<Menu> GetMenuDetailById(int id)
        {
            var menuDto = await GetMenuById(id);
            var dishListDto = await GetDishesByMenuId(id);

            return MenuMapper.MapMenuDetail(menuDto, dishListDto);
        }

        public void AddMenu(MutateMenuDto menu)
        {
            httpClient.PostAsJsonAsync($"{configuration.GetSection("BaseURL").Value}/{ClassConstants.Menu}", menu);
        }

        public void AddMenuDish(int id, MutateDishMenuDto dish)
        {
            httpClient.PostAsJsonAsync($"{configuration.GetSection("BaseURL").Value}/{ClassConstants.Menu}/{id}/{ClassConstants.Dishes}", dish);
        }

        public void DeleteMenu(int id)
        {
            httpClient.DeleteAsync($"{configuration.GetSection("BaseURL").Value}/{ClassConstants.Menu}/{id}");
        }
        public void DeleteMenuDish(DeleteDishMenuDto dish)
        {
            var request = new HttpRequestMessage(HttpMethod.Delete,
                $"{configuration.GetSection("BaseURL").Value}/{ClassConstants.Menu}/{dish.MenuId}/{ClassConstants.Dishes}/{dish.DishId}");
            request.Content = new StringContent(JsonConvert.SerializeObject(dish), Encoding.UTF8, "application/json");
            httpClient.SendAsync(request);
        }

        public void UpdateMenu(MutateMenuDto menu)
        {
            httpClient.PutAsJsonAsync($"{configuration.GetSection("BaseURL").Value}/{ClassConstants.Menu}", menu);
        }

        public void UpdateMenuDish(MutateDishMenuDto dishMenuDto)
        {
            httpClient.PutAsJsonAsync(
                $"{configuration.GetSection("BaseURL").Value}/{ClassConstants.Menu}/{dishMenuDto.Id}/" +
                $"{ClassConstants.Dishes}" +
                $"/{dishMenuDto.Dish.Id}", dishMenuDto);
        }
    }
}
