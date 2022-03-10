using Horeca.Shared.Constants;
using Horeca.Shared.Data.Entities;
using Horeca.Shared.Dtos.Menus;
using Newtonsoft.Json;

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

        public void AddMenu(Menu menu)
        {
            httpClient.PostAsJsonAsync($"{configuration.GetSection("BaseURL").Value}/{ClassConstants.Menu}", menu);
        }

        public void DeleteMenu(int id)
        {
            throw new NotImplementedException();
        }

        public Menu GetMenuById(int id)
        {
            var menu = httpClient.GetAsync($"{configuration.GetSection("BaseURL").Value}/{ClassConstants.Menu}/{id}");
            Console.WriteLine(menu.Result);
            var result = JsonConvert.DeserializeObject<Menu>(menu.Result.Content.ReadAsStringAsync().Result);

            var dishes = httpClient.GetAsync($"{configuration.GetSection("BaseURL").Value}/{ClassConstants.Menu}/{id}/dishes");
            var listResult = JsonConvert.DeserializeObject<MenuDishesByIdDto>(dishes.Result.Content.ReadAsStringAsync().Result);

            result.Dishes = listResult.Dishes.ToList();

            return result;
        }

        public IEnumerable<Menu> GetMenus()
        {
            var menus = httpClient.GetAsync($"{configuration.GetSection("BaseURL").Value}/{ClassConstants.Menu}");
            var result = JsonConvert.DeserializeObject<IEnumerable<Menu>>(menus.Result.Content.ReadAsStringAsync().Result);
            return result;
        }

        public void UpdateMenu(Menu menu)
        {
            httpClient.PutAsJsonAsync($"{configuration.GetSection("BaseURL").Value}/{ClassConstants.Menu}", menu);
        }
    }
}
