using Horeca.MVC.Models.Mappers;
using Horeca.MVC.Services.Interfaces;
using Horeca.Shared.Constants;
using Horeca.Shared.Data.Entities;
using Horeca.Shared.Dtos.Dishes;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace Horeca.MVC.Services
{
    public class DishService : IDishService
    {
        private readonly HttpClient httpClient;
        private readonly IHttpContextAccessor httpContextAccessor;
        private IConfiguration configuration;

        public DishService(HttpClient httpClient, IConfiguration iConfig, IHttpContextAccessor httpContextAccessor)
        {
            this.httpClient = httpClient;
            this.httpContextAccessor = httpContextAccessor;
            configuration = iConfig;
        }
        public void CheckToken()
        {
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",
                httpContextAccessor.HttpContext.Session.GetString("JWToken"));
        }

        public async Task<IEnumerable<DishDto>> GetDishes()
        {
            CheckToken();
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
            CheckToken();
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
            CheckToken();
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
            CheckToken();
            var dishDto = await GetDishById(id);
            var ingredientListDto = await GetIngredientsByDishId(id);
            if (dishDto == null || ingredientListDto == null)
            {
                return null;
            }

            return DishMapper.MapDishDetail(dishDto, ingredientListDto);
        }

        public async Task<HttpResponseMessage> AddDish(MutateDishDto dish)
        {
            CheckToken();
            var request = new HttpRequestMessage(HttpMethod.Post,
                $"{configuration.GetSection("BaseURL").Value}/{ClassConstants.Dish}");

            request.Content = new StringContent(JsonConvert.SerializeObject(dish), Encoding.UTF8, "application/json");

            var response = await httpClient.SendAsync(request);
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }
            return response;
        }

        public async Task<HttpResponseMessage> AddDishIngredient(int id, MutateIngredientByDishDto ingredient)
        {
            CheckToken();
            var request = new HttpRequestMessage(HttpMethod.Post,
                $"{configuration.GetSection("BaseURL").Value}/{ClassConstants.Dish}/{id}/{ClassConstants.Ingredients}");
            request.Content = new StringContent(JsonConvert.SerializeObject(ingredient), Encoding.UTF8, "application/json");

            var response = await httpClient.SendAsync(request);
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }
            return response;
        }

        public async Task<HttpResponseMessage> DeleteDish(int id)
        {
            CheckToken();
            var response = await httpClient.DeleteAsync($"{configuration.GetSection("BaseURL").Value}/{ClassConstants.Dish}/{id}");
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }
            return response;
        }

        public async Task<HttpResponseMessage> DeleteDishIngredient(DeleteIngredientDishDto ingredient)
        {
            CheckToken();
            var request = new HttpRequestMessage(HttpMethod.Delete,
                $"{configuration.GetSection("BaseURL").Value}/{ClassConstants.Dish}/{ingredient.DishId}/" +
                $"{ClassConstants.Ingredients}/{ingredient.IngredientId}");

            request.Content = new StringContent(JsonConvert.SerializeObject(ingredient), Encoding.UTF8, "application/json");
            return await httpClient.SendAsync(request);
        }

        public async Task<HttpResponseMessage> UpdateDish(MutateDishDto dish)
        {
            CheckToken();
            var request = new HttpRequestMessage(HttpMethod.Put,
                $"{configuration.GetSection("BaseURL").Value}/{ClassConstants.Dish}");
            request.Content = new StringContent(JsonConvert.SerializeObject(dish), Encoding.UTF8, "application/json");

            var response = await httpClient.SendAsync(request);
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }
            return response;
        }

        public async Task<HttpResponseMessage> UpdateDishIngredient(MutateIngredientByDishDto ingredient)
        {
            CheckToken();
            var request = new HttpRequestMessage(HttpMethod.Put,
                $"{configuration.GetSection("BaseURL").Value}/{ClassConstants.Dish}/{ingredient.Id}" +
                $"/{ClassConstants.Ingredients}/{ingredient.Ingredient.Id}");
            request.Content = new StringContent(JsonConvert.SerializeObject(ingredient), Encoding.UTF8, "application/json");

            var response = await httpClient.SendAsync(request);
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }
            return response;
        }
    }
}
