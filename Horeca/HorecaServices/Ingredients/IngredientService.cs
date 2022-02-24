using Domain.Kitchen;
using HorecaMVC.Data;
using HorecaPersistence;
using HorecaShared.Ingredients;
using Microsoft.EntityFrameworkCore;

namespace HorecaServices.Ingredients
{
    public class IngredientService : IIngredientService
    {
        private readonly ApplicationDbContext context;

        public IngredientService(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<IngredientResponse.Create> CreateAsync(IngredientRequest.Create request)
        {
            IngredientResponse.Create response = new();

            var ingredient = new Ingredient(request.Ingredient.Name, request.Ingredient.Amount, request.Ingredient.IngredientType);

            context.Ingredients.Add(ingredient);
            await context.SaveChangesAsync();

            response.IngredientId = ingredient.Id;
            return response;
        }

        public async Task DeleteAsync(IngredientRequest.Delete request)
        {
            context.Ingredients.RemoveIf(p => p.Id == request.IngredientId);
            await context.SaveChangesAsync();
        }

        public async Task<IngredientResponse.Edit> EditAsync(IngredientRequest.Edit request)
        {
            IngredientResponse.Edit response = new();

            var ingredient = await context.Ingredients.SingleAsync(x => x.Id == request.IngredientId);
            var model = request.Ingredient;

            MapModelChanges(ingredient, model);

            await context.SaveChangesAsync();
            response.IngredientId = ingredient.Id;

            return response;
        }

        private void MapModelChanges(Ingredient ingredient, IngredientDto.Mutate model)
        {
            ingredient.Name = model.Name;
            ingredient.Amount = model.Amount;
            ingredient.IngredientType = model.IngredientType;
        }

        public async Task<IngredientResponse.GetDetail> GetDetailAsync(IngredientRequest.GetDetail request)
        {
            IngredientResponse.GetDetail response = new();

            response.Ingredient = await context.Ingredients.Select(x =>
            new IngredientDto.Detail
            {
                Id = x.Id,
                Name = x.Name,
                Amount = x.Amount,
                IsEnabled = x.IsEnabled,
                IngredientType = x.IngredientType,
            })
                .SingleAsync(x => x.Id == request.IngredientId);

            return response;
        }

        public async Task<IngredientResponse.GetIndex> GetIndexAsync(IngredientRequest.GetIndex request)
        {
            IngredientResponse.GetIndex response = new();

            var query = context.Ingredients.AsQueryable();

            if (!string.IsNullOrWhiteSpace(request.SearchTerm))
            {
                query = query.Where(x => x.Name.Contains(request.SearchTerm));
            }
            if (request.OnlyActiveIngredients)
                query = query.Where(x => x.IsEnabled);

            response.TotalAmount = query.Count();

            query = query.OrderBy(x => x.Name);
            query = query.Skip(request.Amount * request.Page);
            query = query.Take(request.Amount);

            response.Ingredients = await query.Select(x => new IngredientDto.Index
            {
                Id = x.Id,
                Name = x.Name,
            }).ToListAsync();

            return response;
        }
    }
}