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
        private readonly IConfiguration configuration;

        public DishService(HttpClient httpClient, IConfiguration configuration)
        {
            this.httpClient = httpClient;
            this.configuration = configuration;
        }

        public async Task<IEnumerable<DishDto>> GetDishes()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"{configuration.GetSection("BaseURL").Value}/{ClassConstants.Dish}");

            var response = await httpClient.SendAsync(request);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var result = JsonConvert.DeserializeObject<IEnumerable<DishDto>>(await response.Content.ReadAsStringAsync());
                return result;
            }
            if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                return null;
            }
            return null;
        }

        public async Task<DishDto> GetDishById(int id)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"{configuration.GetSection("BaseURL").Value}/{ClassConstants.Dish}/{id}");

            var response = await httpClient.SendAsync(request);
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }
            var result = JsonConvert.DeserializeObject<DishDto>(await response.Content.ReadAsStringAsync());

            return result;
        }

        public async Task<DishIngredientsByIdDto> GetIngredientsByDishId(int id)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"{configuration.GetSection("BaseURL").Value}/{ClassConstants.Dish}/" +
                $"{id}/{ClassConstants.Ingredients}");

            var response = await httpClient.SendAsync(request);
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }
            var listResult = JsonConvert.DeserializeObject<DishIngredientsByIdDto>(await response.Content.ReadAsStringAsync());

            return listResult;
        }

        public async Task<Dish> GetDishDetailById(int id)
        {
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
            var request = new HttpRequestMessage(HttpMethod.Post,
                $"{configuration.GetSection("BaseURL").Value}/{ClassConstants.Dish}")
            {
                Content = new StringContent(JsonConvert.SerializeObject(dish), Encoding.UTF8, "application/json")
            };

            var response = await httpClient.SendAsync(request);
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }
            return response;
        }

        public async Task<HttpResponseMessage> AddDishIngredient(int id, MutateIngredientByDishDto ingredient)
        {
            var request = new HttpRequestMessage(HttpMethod.Post,
                $"{configuration.GetSection("BaseURL").Value}/{ClassConstants.Dish}/{id}/{ClassConstants.Ingredients}")
            {
                Content = new StringContent(JsonConvert.SerializeObject(ingredient), Encoding.UTF8, "application/json")
            };

            var response = await httpClient.SendAsync(request);
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }
            return response;
        }

        public async Task<HttpResponseMessage> DeleteDish(int id)
        {
            var request = new HttpRequestMessage(HttpMethod.Delete, $"{configuration.GetSection("BaseURL").Value}/{ClassConstants.Dish}/{id}");

            var response = await httpClient.SendAsync(request);
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }
            return response;
        }

        public async Task<HttpResponseMessage> DeleteDishIngredient(DeleteIngredientDishDto ingredient)
        {
            var request = new HttpRequestMessage(HttpMethod.Delete,
                $"{configuration.GetSection("BaseURL").Value}/{ClassConstants.Dish}/{ingredient.DishId}/" +
                $"{ClassConstants.Ingredients}/{ingredient.IngredientId}")
            {
                Content = new StringContent(JsonConvert.SerializeObject(ingredient), Encoding.UTF8, "application/json")
            };
            return await httpClient.SendAsync(request);
        }

        public async Task<HttpResponseMessage> UpdateDish(MutateDishDto dish)
        {
            var request = new HttpRequestMessage(HttpMethod.Put,
                $"{configuration.GetSection("BaseURL").Value}/{ClassConstants.Dish}")
            {
                Content = new StringContent(JsonConvert.SerializeObject(dish), Encoding.UTF8, "application/json")
            };

            var response = await httpClient.SendAsync(request);
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }
            return response;
        }

        public async Task<HttpResponseMessage> UpdateDishIngredient(MutateIngredientByDishDto ingredient)
        {
            var request = new HttpRequestMessage(HttpMethod.Put,
                $"{configuration.GetSection("BaseURL").Value}/{ClassConstants.Dish}/{ingredient.Id}" +
                $"/{ClassConstants.Ingredients}/{ingredient.Ingredient.Id}")
            {
                Content = new StringContent(JsonConvert.SerializeObject(ingredient), Encoding.UTF8, "application/json")
            };

            var response = await httpClient.SendAsync(request);
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }
            return response;
        }
    }
}