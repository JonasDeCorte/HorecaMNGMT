using Horeca.Shared.Constants;
using Horeca.Shared.Data.Entities;
using Newtonsoft.Json;

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

        public async Task<IEnumerable<Ingredient>> GetIngredients()
        {
            HttpResponseMessage response = await httpClient.GetAsync($"{configuration.GetSection("BaseURL").Value}/" +
                $"{ClassConstants.Ingredient}");
            var result = JsonConvert.DeserializeObject<IEnumerable<Ingredient>>(response.Content.ReadAsStringAsync().Result);
            return result;
        }

        public async Task<Ingredient> GetIngredientById(int id)
        {;
            HttpResponseMessage response = await httpClient.GetAsync($"{configuration.GetSection("BaseURL").Value}/" +
                $"{ClassConstants.Ingredient}/{id}");

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            return JsonConvert.DeserializeObject<Ingredient>(response.Content.ReadAsStringAsync().Result);
        }

        public void AddIngredient(Ingredient ingredient)
        {
            httpClient.PostAsJsonAsync($"{configuration.GetSection("BaseURL").Value}/{ClassConstants.Ingredient}", ingredient);
        }

        public void DeleteIngredient(int id)
        {
            httpClient.DeleteAsync($"{configuration.GetSection("BaseURL").Value}/{ClassConstants.Ingredient}/{id}");
        }

        public void UpdateIngredient(Ingredient ingredient)
        {
            httpClient.PutAsJsonAsync($"{configuration.GetSection("BaseURL").Value}/{ClassConstants.Ingredient}", ingredient);
        }
    }
}
