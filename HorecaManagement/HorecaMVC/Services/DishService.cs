using Horeca.Shared.Constants;
using Horeca.Shared.Data.Entities;
using Horeca.Shared.Dtos.Dishes;
using Newtonsoft.Json;

namespace Horeca.MVC.Services
{
    public class DishService : IDishService
    {
        private readonly HttpClient httpClient;
        private IConfiguration configuration;

        public DishService(HttpClient httpClient, IConfiguration iConfig)
        {
            this.httpClient = httpClient;
            configuration = iConfig;
        }

        public IEnumerable<Dish> GetDishes()
        {
            var dishes = httpClient.GetAsync($"{configuration.GetSection("BaseURL").Value}/{ClassConstants.Dish}");
            var result = JsonConvert.DeserializeObject<IEnumerable<Dish>>(dishes.Result.Content.ReadAsStringAsync().Result);
            return result;
        }

        public Dish GetDishById(int id)
        {
            var dish = httpClient.GetAsync($"{configuration.GetSection("BaseURL").Value}/{ClassConstants.Dish}/{id}");
            var result = JsonConvert.DeserializeObject<Dish>(dish.Result.Content.ReadAsStringAsync().Result);

            var ingredients = httpClient.GetAsync($"{configuration.GetSection("BaseURL").Value}/{ClassConstants.Dish}/{id}/ingredients");
            var listResult = JsonConvert.DeserializeObject<DishIngredientsByIdDto>(ingredients.Result.Content.ReadAsStringAsync().Result);

            result.Ingredients = listResult.Ingredients.ToList();

            return result;
        }

        public void AddDish(Dish dish)
        {
            httpClient.PostAsJsonAsync($"{configuration.GetSection("BaseURL").Value}/{ClassConstants.Dish}", dish);
        }

        public void AddDishIngredient(int id, MutateIngredientByDishDto ingredient)
        {
            httpClient.PostAsJsonAsync($"{configuration.GetSection("BaseURL").Value}/{ClassConstants.Dish}/{id}/ingredients", ingredient);
        }

        public void DeleteDish(int id)
        {
            httpClient.DeleteAsync($"{configuration.GetSection("BaseURL").Value}/{ClassConstants.Dish}/{id}");
        }

        public void UpdateDish(Dish dish)
        {
            throw new NotImplementedException();
        }
    }
}
