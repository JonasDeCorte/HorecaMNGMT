﻿using Horeca.Shared.Data.Entities;
using Horeca.Shared.Data.Repositories;
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

        public void Delete(object id)
        {
            httpClient.DeleteAsync($"{configuration.GetSection("BaseURL").Value}/{id}");
        }

        public Ingredient Get(object id)
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
            throw new NotImplementedException();
        }
    }
}
