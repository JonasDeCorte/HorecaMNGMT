using Horeca.MVC.Helpers.Mappers;
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
        private readonly IRestaurantService restaurantService;

        public DishService(HttpClient httpClient, IConfiguration configuration, IRestaurantService restaurantService)
        {
            this.httpClient = httpClient;
            this.configuration = configuration;
            this.restaurantService = restaurantService;
        }

        public async Task<IEnumerable<DishDto>> GetDishes()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, 
                $"{configuration.GetSection("BaseURL").Value}/{ClassConstants.Dish}/{ClassConstants.Restaurant}" +
                $"?{ClassConstants.RestaurantId}={restaurantService.GetCurrentRestaurantId()}");

            var response = await httpClient.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                var result = JsonConvert.DeserializeObject<IEnumerable<DishDto>>(await response.Content.ReadAsStringAsync());
                if (result == null)
                {
                    return new List<DishDto>();
                }
                return result;
            }
            return null;
        }

        public async Task<DishDto> GetDishById(int id)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, 
                $"{configuration.GetSection("BaseURL").Value}/{ClassConstants.Dish}/{ClassConstants.Id}/{ClassConstants.Restaurant}" +
                $"?id={id}&{ClassConstants.RestaurantId}={restaurantService.GetCurrentRestaurantId()}");

            var response = await httpClient.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                var result = JsonConvert.DeserializeObject<DishDto>(await response.Content.ReadAsStringAsync());
                if (result == null)
                {
                    return new DishDto();
                }
                return result;
            }
            return null;
        }

        public async Task<DishIngredientsByIdDto> GetIngredientsByDishId(int id)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, 
                $"{configuration.GetSection("BaseURL").Value}/{ClassConstants.Dish}/{ClassConstants.Ingredients}/{ClassConstants.Restaurant}" +
                $"?id={id}&{ClassConstants.RestaurantId}={restaurantService.GetCurrentRestaurantId()}");

            var response = await httpClient.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                var result = JsonConvert.DeserializeObject<DishIngredientsByIdDto>(await response.Content.ReadAsStringAsync());
                if (result == null)
                {
                    return new DishIngredientsByIdDto();
                }
                return result;
            }
            return null;
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
                $"{configuration.GetSection("BaseURL").Value}/{ClassConstants.Dish}/{ClassConstants.Restaurant}" +
                $"?{ClassConstants.RestaurantId}={restaurantService.GetCurrentRestaurantId()}")
            {
                Content = new StringContent(JsonConvert.SerializeObject(dish), Encoding.UTF8, "application/json")
            };

            var response = await httpClient.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                return response;
            }
            return null;
        }

        public async Task<HttpResponseMessage> AddDishIngredient(int id, MutateIngredientByDishDto ingredient)
        {
            var request = new HttpRequestMessage(HttpMethod.Post,
                $"{configuration.GetSection("BaseURL").Value}/{ClassConstants.Dish}/{ClassConstants.Ingredients}/{ClassConstants.Restaurant}" +
                $"?id={id}&{ClassConstants.RestaurantId}={restaurantService.GetCurrentRestaurantId()}")
            {
                Content = new StringContent(JsonConvert.SerializeObject(ingredient), Encoding.UTF8, "application/json")
            };

            var response = await httpClient.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                return response;
            }
            return null;
        }

        public async Task<HttpResponseMessage> DeleteDish(int id)
        {
            var request = new HttpRequestMessage(HttpMethod.Delete, $"{configuration.GetSection("BaseURL").Value}/{ClassConstants.Dish}?id={id}");

            var response = await httpClient.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                return response;
            }
            return null;
        }

        public async Task<HttpResponseMessage> DeleteDishIngredient(DeleteIngredientDishDto ingredientDto)
        {
            var request = new HttpRequestMessage(HttpMethod.Delete,
                $"{configuration.GetSection("BaseURL").Value}/{ClassConstants.Dish}/{ClassConstants.Ingredients}/{ClassConstants.Restaurant}" +
                $"?id={ingredientDto.DishId}&{ClassConstants.IngredientId}={ingredientDto.IngredientId}&{ClassConstants.RestaurantId}={restaurantService.GetCurrentRestaurantId()}")
            {
                Content = new StringContent(JsonConvert.SerializeObject(ingredientDto), Encoding.UTF8, "application/json")
            };
            return await httpClient.SendAsync(request);
        }

        public async Task<HttpResponseMessage> UpdateDish(MutateDishDto dishDto)
        {
            var request = new HttpRequestMessage(HttpMethod.Put,
                $"{configuration.GetSection("BaseURL").Value}/{ClassConstants.Dish}/{ClassConstants.Restaurant}/" +
                $"?id={dishDto.Id}&{ClassConstants.RestaurantId}={restaurantService.GetCurrentRestaurantId()}")
            {
                Content = new StringContent(JsonConvert.SerializeObject(dishDto), Encoding.UTF8, "application/json")
            };

            var response = await httpClient.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                return response;
            }
            return null;
        }

        public async Task<HttpResponseMessage> UpdateDishIngredient(MutateIngredientByDishDto ingredientByDishDto)
        {
            var request = new HttpRequestMessage(HttpMethod.Put,
                $"{configuration.GetSection("BaseURL").Value}/{ClassConstants.Dish}/{ClassConstants.Ingredients}/{ClassConstants.Restaurant}" +
                $"?id={ingredientByDishDto.Id}&{ClassConstants.IngredientId}={ingredientByDishDto.Ingredient.Id}&{ClassConstants.RestaurantId}={restaurantService.GetCurrentRestaurantId()}")
            {
                Content = new StringContent(JsonConvert.SerializeObject(ingredientByDishDto), Encoding.UTF8, "application/json")
            };

            var response = await httpClient.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                return response;
            }
            return null;
        }
    }
}