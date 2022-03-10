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

        public IEnumerable<Menu> GetMenus()
        {
            var menus = httpClient.GetAsync($"{configuration.GetSection("BaseURL").Value}/{ClassConstants.Menu}");
            var result = JsonConvert.DeserializeObject<IEnumerable<Menu>>(menus.Result.Content.ReadAsStringAsync().Result);
            return result;
        }

        public Menu GetMenuById(int id)
        {
            var menu = httpClient.GetAsync($"{configuration.GetSection("BaseURL").Value}/{ClassConstants.Menu}/{id}");
            var result = JsonConvert.DeserializeObject<Menu>(menu.Result.Content.ReadAsStringAsync().Result);
            var listResult = GetMenuDishesById(id);

            result.Dishes = listResult.Dishes.ToList();

            return result;
        }

        public MenuDishesByIdDto GetMenuDishesById(int id)
        {
            var dishes = httpClient.GetAsync($"{configuration.GetSection("BaseURL").Value}/{ClassConstants.Menu}/{id}/{ClassConstants.Dishes}");
            var listResult = JsonConvert.DeserializeObject<MenuDishesByIdDto>(dishes.Result.Content.ReadAsStringAsync().Result);

            return listResult;
        }

        public void AddMenu(Menu menu)
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

        public void UpdateMenu(Menu menu)
        {
            httpClient.PutAsJsonAsync($"{configuration.GetSection("BaseURL").Value}/{ClassConstants.Menu}", menu);
        }
    }
}
