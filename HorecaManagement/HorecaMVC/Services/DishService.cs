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

        public DishService(HttpClient httpClient, IConfiguration configuration)
        {
            this.httpClient = httpClient;
            this.configuration = configuration;
        }

        public void AddToHeader(HttpRequestMessage request, string token)
        {
            request.Headers.Add("Authorization", token);
        }
        
        public async Task<IEnumerable<DishDto>> GetDishes(string token)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"{configuration.GetSection("BaseURL").Value}/{ClassConstants.Dish}");
            AddToHeader(request, token);

            var response = await httpClient.SendAsync(request);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var result = JsonConvert.DeserializeObject<IEnumerable<DishDto>>(response.Content.ReadAsStringAsync().Result);
                return result;
            }
            if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                Console.WriteLine("Unauthorized");
            }
            return null;
        }

        public async Task<DishDto> GetDishById(int id, string token)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"{configuration.GetSection("BaseURL").Value}/{ClassConstants.Dish}/{id}");
            AddToHeader(request, token);
            var response = await httpClient.SendAsync(request);
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }
            var result = JsonConvert.DeserializeObject<DishDto>(response.Content.ReadAsStringAsync().Result);

            return result;
        }

        public async Task<DishIngredientsByIdDto> GetIngredientsByDishId(int id, string token)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"{configuration.GetSection("BaseURL").Value}/{ClassConstants.Dish}/" +
                $"{id}/{ClassConstants.Ingredients}");
            AddToHeader(request, token);
            var response = await httpClient.SendAsync(request);
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }
            var listResult = JsonConvert.DeserializeObject<DishIngredientsByIdDto>(response.Content.ReadAsStringAsync().Result);

            return listResult;
        }

        public async Task<Dish> GetDishDetailById(int id, string token)
        {
            var dishDto = await GetDishById(id, token);
            var ingredientListDto = await GetIngredientsByDishId(id, token);
            if (dishDto == null || ingredientListDto == null)
            {
                return null;
            }

            return DishMapper.MapDishDetail(dishDto, ingredientListDto);
        }

        public async Task<HttpResponseMessage> AddDish(MutateDishDto dish, string token)
        {
            var request = new HttpRequestMessage(HttpMethod.Post,
                $"{configuration.GetSection("BaseURL").Value}/{ClassConstants.Dish}");
            AddToHeader(request, token);
            request.Content = new StringContent(JsonConvert.SerializeObject(dish), Encoding.UTF8, "application/json");

            var response = await httpClient.SendAsync(request);
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }
            return response;
        }

        public async Task<HttpResponseMessage> AddDishIngredient(int id, MutateIngredientByDishDto ingredient, string token)
        {
            var request = new HttpRequestMessage(HttpMethod.Post,
                $"{configuration.GetSection("BaseURL").Value}/{ClassConstants.Dish}/{id}/{ClassConstants.Ingredients}");
            request.Content = new StringContent(JsonConvert.SerializeObject(ingredient), Encoding.UTF8, "application/json");
            AddToHeader(request, token);

            var response = await httpClient.SendAsync(request);
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }
            return response;
        }

        public async Task<HttpResponseMessage> DeleteDish(int id, string token)
        {
            var request = new HttpRequestMessage(HttpMethod.Delete, $"{configuration.GetSection("BaseURL").Value}/{ClassConstants.Dish}/{id}");
            AddToHeader(request, token);
            var response = await httpClient.SendAsync(request);
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }
            return response;
        }

        public async Task<HttpResponseMessage> DeleteDishIngredient(DeleteIngredientDishDto ingredient, string token)
        {
            var request = new HttpRequestMessage(HttpMethod.Delete,
                $"{configuration.GetSection("BaseURL").Value}/{ClassConstants.Dish}/{ingredient.DishId}/" +
                $"{ClassConstants.Ingredients}/{ingredient.IngredientId}");
            AddToHeader(request, token);
            request.Content = new StringContent(JsonConvert.SerializeObject(ingredient), Encoding.UTF8, "application/json");
            return await httpClient.SendAsync(request);
        }

        public async Task<HttpResponseMessage> UpdateDish(MutateDishDto dish, string token)
        {
            var request = new HttpRequestMessage(HttpMethod.Put,
                $"{configuration.GetSection("BaseURL").Value}/{ClassConstants.Dish}");
            request.Content = new StringContent(JsonConvert.SerializeObject(dish), Encoding.UTF8, "application/json");
            AddToHeader(request, token);
            var response = await httpClient.SendAsync(request);
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }
            return response;
        }

        public async Task<HttpResponseMessage> UpdateDishIngredient(MutateIngredientByDishDto ingredient, string token)
        {
            var request = new HttpRequestMessage(HttpMethod.Put,
                $"{configuration.GetSection("BaseURL").Value}/{ClassConstants.Dish}/{ingredient.Id}" +
                $"/{ClassConstants.Ingredients}/{ingredient.Ingredient.Id}");
            request.Content = new StringContent(JsonConvert.SerializeObject(ingredient), Encoding.UTF8, "application/json");
            AddToHeader(request, token);
            var response = await httpClient.SendAsync(request);
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }
            return response;
        }
    }
}
