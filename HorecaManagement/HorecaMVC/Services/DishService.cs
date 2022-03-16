using Horeca.MVC.Models.Mappers;
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

        public async Task<IEnumerable<DishDto>> GetDishes()
        {
            var response = await httpClient.GetAsync($"{configuration.GetSection("BaseURL").Value}/{ClassConstants.Dish}");

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            var result = JsonConvert.DeserializeObject<IEnumerable<DishDto>>(response.Content.ReadAsStringAsync().Result);
            return result;
        }

        public async Task<DishDto> GetDishById(int id)
        {
            var response = await httpClient.GetAsync($"{configuration.GetSection("BaseURL").Value}/{ClassConstants.Dish}/{id}");
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }
            var result = JsonConvert.DeserializeObject<DishDto>(response.Content.ReadAsStringAsync().Result);

            return result;
        }

        public async Task<DishIngredientsByIdDto> GetIngredientsByDishId(int id)
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

        public async Task<Dish> GetDishDetailById(int id)
        {
            var dishDto = await GetDishById(id);
            var ingredientListDto = await GetIngredientsByDishId(id);

            return DishMapper.MapDishDetail(dishDto, ingredientListDto);
        }

        public void AddDish(MutateDishDto dish)
        {
            var request = new HttpRequestMessage(HttpMethod.Post,
                $"{configuration.GetSection("BaseURL").Value}/{ClassConstants.Dish}");

            request.Content = new StringContent(JsonConvert.SerializeObject(dish), Encoding.UTF8, "application/json");
            httpClient.SendAsync(request);
        }

        public void AddDishIngredient(int id, MutateIngredientByDishDto ingredient)
        {
            var request = new HttpRequestMessage(HttpMethod.Post,
                $"{configuration.GetSection("BaseURL").Value}/{ClassConstants.Dish}/{id}/{ClassConstants.Ingredients}");

            request.Content = new StringContent(JsonConvert.SerializeObject(ingredient), Encoding.UTF8, "application/json");
            httpClient.SendAsync(request);
        }

        public void DeleteDish(int id)
        {
            httpClient.DeleteAsync($"{configuration.GetSection("BaseURL").Value}/{ClassConstants.Dish}/{id}");
        }
        public void DeleteDishIngredient(DeleteIngredientDishDto ingredient)
        {
            var request = new HttpRequestMessage(HttpMethod.Delete,
                $"{configuration.GetSection("BaseURL").Value}/{ClassConstants.Dish}/{ingredient.DishId}/" +
                $"{ClassConstants.Ingredients}/{ingredient.IngredientId}");

            request.Content = new StringContent(JsonConvert.SerializeObject(ingredient), Encoding.UTF8, "application/json");
            httpClient.SendAsync(request);
        }

        public void UpdateDish(MutateDishDto dish)
        {
            var request = new HttpRequestMessage(HttpMethod.Put,
                $"{configuration.GetSection("BaseURL").Value}/{ClassConstants.Dish}");

            request.Content = new StringContent(JsonConvert.SerializeObject(dish), Encoding.UTF8, "application/json");
            httpClient.SendAsync(request);
        }

        public void UpdateDishIngredient(MutateIngredientByDishDto ingredient)
        {
            var request = new HttpRequestMessage(HttpMethod.Put,
                $"{configuration.GetSection("BaseURL").Value}/{ClassConstants.Dish}/{ingredient.Id}" +
                $"/{ClassConstants.Ingredients}/{ingredient.Ingredient.Id}");

            request.Content = new StringContent(JsonConvert.SerializeObject(ingredient), Encoding.UTF8, "application/json");
            httpClient.SendAsync(request);
        }
    }
}
