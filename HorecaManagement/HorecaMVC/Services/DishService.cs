using Horeca.Shared.Constants;
using Horeca.Shared.Data.Entities;
using Horeca.Shared.Data.Repositories;
using Horeca.Shared.Dtos.Dishes;
using Newtonsoft.Json;

namespace Horeca.MVC.Services
{
    public class DishService : IDishRepository
    {
        private readonly HttpClient httpClient;
        private IConfiguration configuration;

        public DishService(HttpClient httpClient, IConfiguration iConfig)
        {
            this.httpClient = httpClient;
            configuration = iConfig;
        }

        public void Add(Dish entity)
        {
            throw new NotImplementedException();
        }

        public int Count()
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Dish Get(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Dish> GetAll()
        {
            var dishes = httpClient.GetAsync($"{configuration.GetSection("BaseURL").Value}/{ClassConstants.Dish}");
            var result = JsonConvert.DeserializeObject<IEnumerable<Dish>>(dishes.Result.Content.ReadAsStringAsync().Result);
            return result;
        }

        public Dish GetDishIncludingDependencies(int id)
        {
            throw new NotImplementedException();
        }

        public DishDtoDetailDto GetIncludingDependencies(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(Dish entity)
        {
            throw new NotImplementedException();
        }
    }
}
