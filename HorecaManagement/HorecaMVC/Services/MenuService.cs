using Horeca.Shared.Constants;
using Horeca.Shared.Data.Entities;
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
            throw new NotImplementedException();
        }

        public void DeleteMenu(int id)
        {
            throw new NotImplementedException();
        }

        public Menu GetMenuById(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Menu> GetMenus()
        {
            var menus = httpClient.GetAsync($"{configuration.GetSection("BaseURL").Value}/{ClassConstants.Menu}");
            var result = JsonConvert.DeserializeObject<IEnumerable<Menu>>(menus.Result.Content.ReadAsStringAsync().Result);
            return result;
        }

        public void UpdateMenu(Menu menu)
        {
            throw new NotImplementedException();
        }
    }
}
