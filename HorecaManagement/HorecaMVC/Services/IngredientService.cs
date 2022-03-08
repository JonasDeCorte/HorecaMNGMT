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

        public IEnumerable<Ingredient> GetIngredients()
        {
            var ingredients = httpClient.GetAsync($"{configuration.GetSection("BaseURL").Value}/{ClassConstants.Ingredient}");
            var result = JsonConvert.DeserializeObject<IEnumerable<Ingredient>>(ingredients.Result.Content.ReadAsStringAsync().Result);
            return result;
        }

        public Ingredient GetIngredientById(int id)
        {
            var ingredient = httpClient.GetAsync($"{configuration.GetSection("BaseURL").Value}/{ClassConstants.Ingredient}/{id}");
            var result = JsonConvert.DeserializeObject<Ingredient>(ingredient.Result.Content.ReadAsStringAsync().Result);
            return result;
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
