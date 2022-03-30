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
        private IConfiguration configuration;
        private readonly ITokenService tokenService;

        public IngredientService(HttpClient httpClient, IConfiguration configuration, ITokenService tokenService)
        {
            this.httpClient = httpClient;
            this.configuration = configuration;
            this.tokenService = tokenService;
        }

        public async Task<IEnumerable<IngredientDto>> GetIngredients()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"{configuration.GetSection("BaseURL").Value}/" +
                $"{ClassConstants.Ingredient}");
            tokenService.AddTokenToHeader(request);

            var response = await httpClient.SendAsync(request);
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            return JsonConvert.DeserializeObject<IEnumerable<IngredientDto>>(response.Content.ReadAsStringAsync().Result);
        }

        public async Task<IngredientDto> GetIngredientById(int id)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"{configuration.GetSection("BaseURL").Value}/" +
                $"{ClassConstants.Ingredient}/{id}");
            tokenService.AddTokenToHeader(request);

            var response = await httpClient.SendAsync(request);
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }
            return JsonConvert.DeserializeObject<IngredientDto>(response.Content.ReadAsStringAsync().Result);
        }

        public async Task<HttpResponseMessage> AddIngredient(MutateIngredientDto ingredient)
        {
            var request = new HttpRequestMessage(HttpMethod.Post,
                $"{configuration.GetSection("BaseURL").Value}/{ClassConstants.Ingredient}");
            request.Content = new StringContent(JsonConvert.SerializeObject(ingredient), Encoding.UTF8, "application/json");
            tokenService.AddTokenToHeader(request);

            var response = await httpClient.SendAsync(request);
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }
            return response;
        }

        public async Task<HttpResponseMessage> DeleteIngredient(int id)
        {
            var request = new HttpRequestMessage(HttpMethod.Delete,
                $"{configuration.GetSection("BaseURL").Value}/{ClassConstants.Ingredient}/{id}");
            tokenService.AddTokenToHeader(request);

            var response = await httpClient.SendAsync(request);
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }
            return response;
        }

        public async Task<HttpResponseMessage> UpdateIngredient(MutateIngredientDto ingredient)
        {
            var request = new HttpRequestMessage(HttpMethod.Put,
                $"{configuration.GetSection("BaseURL").Value}/{ClassConstants.Ingredient}");
            request.Content = new StringContent(JsonConvert.SerializeObject(ingredient), Encoding.UTF8, "application/json");
            tokenService.AddTokenToHeader(request);

            var response = await httpClient.SendAsync(request);
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }
            return response;
        }
    }
}
