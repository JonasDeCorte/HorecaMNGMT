using Horeca.MVC.Services.Interfaces;
using Horeca.Shared.Constants;
using Horeca.Shared.Dtos.Ingredients;
using Newtonsoft.Json;
using System.Text;

namespace Horeca.MVC.Services
{
    public class IngredientService : IIngredientService
    {
        private readonly HttpClient httpClient;
        private readonly IConfiguration configuration;
        private readonly IRestaurantService restaurantService;

        public IngredientService(HttpClient httpClient, IConfiguration configuration, IRestaurantService restaurantService)
        {
            this.httpClient = httpClient;
            this.configuration = configuration;
            this.restaurantService = restaurantService;
        }

        public async Task<IEnumerable<IngredientDto>> GetIngredients()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"{configuration.GetSection("BaseURL").Value}/" +
                $"{ClassConstants.Ingredient}/{ClassConstants.All}/{ClassConstants.Restaurant}" +
                $"?{ClassConstants.RestaurantId}={restaurantService.GetCurrentRestaurantId()}");

            var response = await httpClient.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                var result = JsonConvert.DeserializeObject<IEnumerable<IngredientDto>>(await response.Content.ReadAsStringAsync());
                if (result == null)
                {
                    return new List<IngredientDto>();
                }
                return result;
            }
            return null;
        }

        public async Task<IngredientDto> GetIngredientById(int id)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"{configuration.GetSection("BaseURL").Value}/" +
                $"{ClassConstants.Ingredient}/{ClassConstants.Id}/{ClassConstants.Restaurant}" +
                $"?id={id}&{ClassConstants.RestaurantId}={restaurantService.GetCurrentRestaurantId()}");

            var response = await httpClient.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                var result = JsonConvert.DeserializeObject<IngredientDto>(await response.Content.ReadAsStringAsync());
                if (result == null)
                {
                    return new IngredientDto();
                }
                return result;
            }
            return null;
        }

        public async Task<HttpResponseMessage> AddIngredient(MutateIngredientDto ingredient)
        {
            var request = new HttpRequestMessage(HttpMethod.Post,
                $"{configuration.GetSection("BaseURL").Value}/{ClassConstants.Ingredient}/{ClassConstants.Restaurant}" +
                $"?{ClassConstants.RestaurantId}={restaurantService.GetCurrentRestaurantId()}")
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

        public async Task<HttpResponseMessage> DeleteIngredient(int id)
        {
            var request = new HttpRequestMessage(HttpMethod.Delete,
                $"{configuration.GetSection("BaseURL").Value}/{ClassConstants.Ingredient}?id={id}");

            var response = await httpClient.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                return response;
            }
            return null;
        }

        public async Task<HttpResponseMessage> UpdateIngredient(MutateIngredientDto ingredient)
        {
            var request = new HttpRequestMessage(HttpMethod.Put,
                $"{configuration.GetSection("BaseURL").Value}/{ClassConstants.Ingredient}/{ClassConstants.Restaurant}" +
                $"?id={ingredient.Id}&{ClassConstants.RestaurantId}={restaurantService.GetCurrentRestaurantId()}")
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
    }
}