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

        public IngredientService(HttpClient httpClient, IConfiguration iConfig)
        {
            this.httpClient = httpClient;
            configuration = iConfig;
        }

        public async Task<IEnumerable<IngredientDto>> GetIngredients()
        {
            HttpResponseMessage response = await httpClient.GetAsync($"{configuration.GetSection("BaseURL").Value}/" +
                $"{ClassConstants.Ingredient}");
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            return JsonConvert.DeserializeObject<IEnumerable<IngredientDto>>(response.Content.ReadAsStringAsync().Result);
        }

        public async Task<IngredientDto> GetIngredientById(int id)
        {
            HttpResponseMessage response = await httpClient.GetAsync($"{configuration.GetSection("BaseURL").Value}/" +
                $"{ClassConstants.Ingredient}/{id}");
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            return JsonConvert.DeserializeObject<IngredientDto>(response.Content.ReadAsStringAsync().Result);
        }

        public void AddIngredient(MutateIngredientDto ingredient)
        {
            var request = new HttpRequestMessage(HttpMethod.Post,
                $"{configuration.GetSection("BaseURL").Value}/{ClassConstants.Ingredient}");

            request.Content = new StringContent(JsonConvert.SerializeObject(ingredient), Encoding.UTF8, "application/json");
            httpClient.SendAsync(request);
        }

        public void DeleteIngredient(int id)
        {
            httpClient.DeleteAsync($"{configuration.GetSection("BaseURL").Value}/{ClassConstants.Ingredient}/{id}");
        }

        public void UpdateIngredient(MutateIngredientDto ingredient)
        {
            var request = new HttpRequestMessage(HttpMethod.Put,
                $"{configuration.GetSection("BaseURL").Value}/{ClassConstants.Ingredient}");

            request.Content = new StringContent(JsonConvert.SerializeObject(ingredient), Encoding.UTF8, "application/json");
            httpClient.SendAsync(request);
        }
    }
}
