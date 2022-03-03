using Horeca.Shared.Data.Entities;
using Horeca.Shared.Data.Repositories;
using Newtonsoft.Json;

namespace HorecaMVC.Services
{
    public class IngredientService : IIngredientRepository
    {
        private readonly HttpClient httpClient;

        public IngredientService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public void Add(Ingredient entity)
        {
            throw new NotImplementedException();
        }

        public int Count()
        {
            throw new NotImplementedException();
        }

        public void Delete(object id)
        {
            httpClient.DeleteAsync($"https://localhost:7282/api/Ingredients/{id}");
        }

        public Ingredient Get(object id)
        {
            Console.WriteLine($"start of get(object id) id: {id}");
            var ingredient = httpClient.GetAsync($"https://localhost:7282/api/Ingredients/{id}");
            Console.WriteLine("after getasync");
            var result = JsonConvert.DeserializeObject<Ingredient>(ingredient.Result.Content.ReadAsStringAsync().Result);
            Console.WriteLine("after deserialize");
            return result;
        }

        public IEnumerable<Ingredient> GetAll()
        {
            var ingredients = httpClient.GetAsync("https://localhost:7282/api/Ingredients");
            var result = JsonConvert.DeserializeObject<IEnumerable<Ingredient>>(ingredients.Result.Content.ReadAsStringAsync().Result);
            return result;
        }

        public void Update(Ingredient entity)
        {
            throw new NotImplementedException();
        }
    }
}
