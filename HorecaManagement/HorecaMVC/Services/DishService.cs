using Horeca.Shared.Constants;
using Horeca.Shared.Data.Entities;
using Horeca.Shared.Dtos.Dishes;
using Newtonsoft.Json;
using System.Text;

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
        public void DeleteDishIngredient(DeleteIngredientDishDto ingredient)
        {
            var request = new HttpRequestMessage(HttpMethod.Delete,
                $"{configuration.GetSection("BaseURL").Value}/{ClassConstants.Dish}/{ingredient.DishId}/ingredients/{ingredient.IngredientId}");
            request.Content = new StringContent(JsonConvert.SerializeObject(ingredient), Encoding.UTF8, "application/json");
            httpClient.SendAsync(request);
        }

        public void UpdateDish(Dish dish)
        {
            httpClient.PutAsJsonAsync($"{configuration.GetSection("BaseURL").Value}/{ClassConstants.Dish}", dish);
        }
    }
}
