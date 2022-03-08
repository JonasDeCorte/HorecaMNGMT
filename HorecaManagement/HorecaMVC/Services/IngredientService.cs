using Horeca.Shared.Data.Entities;
using Horeca.Shared.Data.Repositories;
using Horeca.Shared.Dtos;
using Newtonsoft.Json;

namespace HorecaMVC.Services
{
    public class IngredientService : IIngredientRepository
    {
        private readonly HttpClient httpClient;
        private IConfiguration configuration;

        public IngredientService(HttpClient httpClient, IConfiguration iConfig)
        {
            this.httpClient = httpClient;
            configuration = iConfig;
        }

        public void Add(Ingredient entity)
        {
            httpClient.PostAsJsonAsync($"{configuration.GetSection("BaseURL").Value}", entity);
        }

        public int Count()
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            httpClient.DeleteAsync($"{configuration.GetSection("BaseURL").Value}/{id}");
        }

        public Ingredient Get(int id)
        {
            var ingredient = httpClient.GetAsync($"{configuration.GetSection("BaseURL").Value}/{id}");
            var result = JsonConvert.DeserializeObject<Ingredient>(ingredient.Result.Content.ReadAsStringAsync().Result);
            return result;
        }

        public IEnumerable<Ingredient> GetAll()
        {
            var ingredients = httpClient.GetAsync($"{configuration.GetSection("BaseURL").Value}");
            var result = JsonConvert.DeserializeObject<IEnumerable<Ingredient>>(ingredients.Result.Content.ReadAsStringAsync().Result);
            return result;
        }

        public void Update(Ingredient entity)
        {
            Console.WriteLine("Ingredient Id: " + entity.Id);
            Console.WriteLine("Unit Id: " + entity.Unit.Id);

            Console.WriteLine("before put");
            httpClient.PutAsJsonAsync($"{configuration.GetSection("BaseURL").Value}", entity);
            Console.WriteLine("after put");

            Ingredient test = Get(entity.Id);

            Console.WriteLine("Ingredient Id: " + test.Id);
            Console.WriteLine("Unit Id: " + test.Unit.Id);
        }

        public IEnumerable<IngredientDto> GetAllIncludingUnit()
        {
            var ingredients = httpClient.GetAsync($"{configuration.GetSection("BaseURL").Value}");
            var result = JsonConvert.DeserializeObject<IEnumerable<IngredientDto>>(ingredients.Result.Content.ReadAsStringAsync().Result);
            return result;
        }

        public IngredientDto GetIncludingUnit(int id)
        {
            var ingredient = httpClient.GetAsync($"{configuration.GetSection("BaseURL").Value}/{id}");
            var result = JsonConvert.DeserializeObject<IngredientDto>(ingredient.Result.Content.ReadAsStringAsync().Result);
            return result;
        }

        public Ingredient GetIngredientIncludingUnit(int id)
        {
            var ingredient = httpClient.GetAsync($"{configuration.GetSection("BaseURL").Value}/{id}");
            var result = JsonConvert.DeserializeObject<Ingredient>(ingredient.Result.Content.ReadAsStringAsync().Result);
            return result;
        }
    }
}
