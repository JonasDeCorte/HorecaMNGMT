using Horeca.MVC.Services.Interfaces;
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

        public async Task<IEnumerable<Dish>> GetDishes()
        {
            var response = await httpClient.GetAsync($"{configuration.GetSection("BaseURL").Value}/{ClassConstants.Dish}");
            var result = JsonConvert.DeserializeObject<IEnumerable<Dish>>(response.Content.ReadAsStringAsync().Result);
            return result;
        }

        public async Task<Dish> GetDishById(int id)
        {
            var response = await httpClient.GetAsync($"{configuration.GetSection("BaseURL").Value}/{ClassConstants.Dish}/{id}");

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            var result = JsonConvert.DeserializeObject<Dish>(response.Content.ReadAsStringAsync().Result);

            var listResult = await GetDishIngredientsById(id);
            result.Ingredients = listResult.Ingredients.ToList();

            return result;
        }

        public async Task<DishIngredientsByIdDto> GetDishIngredientsById(int id)
        {
            var response = await httpClient.GetAsync($"{configuration.GetSection("BaseURL").Value}/{ClassConstants.Dish}/" +
                $"{id}/{ClassConstants.Ingredients}");

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            var listResult = JsonConvert.DeserializeObject<DishIngredientsByIdDto>(response.Content.ReadAsStringAsync()
                .Result);

            return listResult;
        }

        public void AddDish(Dish dish)
        {
            httpClient.PostAsJsonAsync($"{configuration.GetSection("BaseURL").Value}/{ClassConstants.Dish}", dish);
        }

        public void AddDishIngredient(int id, MutateIngredientByDishDto ingredient)
        {
            httpClient.PostAsJsonAsync($"{configuration.GetSection("BaseURL").Value}/{ClassConstants.Dish}/{id}/{ClassConstants.Ingredients}", ingredient);
        }

        public void DeleteDish(int id)
        {
            httpClient.DeleteAsync($"{configuration.GetSection("BaseURL").Value}/{ClassConstants.Dish}/{id}");
        }
        public void DeleteDishIngredient(DeleteIngredientDishDto ingredient)
        {
            var request = new HttpRequestMessage(HttpMethod.Delete,
                $"{configuration.GetSection("BaseURL").Value}/{ClassConstants.Dish}/{ingredient.DishId}/{ClassConstants.Ingredients}/{ingredient.IngredientId}");
            request.Content = new StringContent(JsonConvert.SerializeObject(ingredient), Encoding.UTF8, "application/json");
            httpClient.SendAsync(request);
        }

        public void UpdateDish(Dish dish)
        {
            httpClient.PutAsJsonAsync($"{configuration.GetSection("BaseURL").Value}/{ClassConstants.Dish}", dish);
        }

        public void UpdateDishIngredient(MutateIngredientByDishDto ingredient)
        {
            httpClient.PutAsJsonAsync(
                $"{configuration.GetSection("BaseURL").Value}/{ClassConstants.Dish}/{ingredient.Id}/{ClassConstants.Ingredients}" +
                $"/{ingredient.Ingredient.Id}", ingredient);
        }
    }
}
