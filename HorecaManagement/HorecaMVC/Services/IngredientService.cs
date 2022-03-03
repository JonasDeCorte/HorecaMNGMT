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
            throw new NotImplementedException();
        }

        public Ingredient Get(object id)
        {
            throw new NotImplementedException();
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
