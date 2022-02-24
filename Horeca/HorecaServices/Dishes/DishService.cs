using Domain.Kitchen;
using HorecaMVC.Data;
using HorecaPersistence;
using HorecaShared.Dishes;
using Microsoft.EntityFrameworkCore;

namespace HorecaServices.Dishes
{
    public class DishService : IDishService
    {
        private readonly ApplicationDbContext context;

        public DishService(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<DishResponse.Create> CreateAsync(DishRequest.Create request)
        {
            DishResponse.Create response = new();

            var dish = new Dish(request.Dish.Name, request.Dish.Category, request.Dish.Description, request.Dish.DishType);

            context.Dishes.Add(dish);
            await context.SaveChangesAsync();

            response.DishId = dish.Id;
            return response;
        }

        public async Task DeleteAsync(DishRequest.Delete request)
        {
            context.Dishes.RemoveIf(p => p.Id == request.DishId);
            await context.SaveChangesAsync();
        }

        public async Task<DishResponse.Edit> EditAsync(DishRequest.Edit request)
        {
            DishResponse.Edit response = new();

            var dish = await context.Dishes.SingleAsync(x => x.Id == request.DishId);
            var model = request.Dish;

            MapModelChanges(dish, model);

            await context.SaveChangesAsync();
            response.DishId = dish.Id;

            return response;
        }

        private void MapModelChanges(Dish dish, DishDto.Mutate model)
        {
            dish.Name = model.Name;
            dish.Category = model.Category;
            dish.Description = model.Description;
            dish.Category = model.Category;
        }

        public async Task<DishResponse.GetDetail> GetDetailAsync(DishRequest.GetDetail request)
        {
            DishResponse.GetDetail response = new();

            response.Dish = await context.Dishes.Select(x =>
            new DishDto.Detail
            {
                Id = x.Id,
                Name = x.Name,
                Category = x.Category,
                Description = x.Description,
                DishType = x.DishType,
            }).SingleAsync(x => x.Id == request.DishId);

            return response;
        }

        public async Task<DishResponse.GetIndex> GetIndexAsync(DishRequest.GetIndex request)
        {
            DishResponse.GetIndex response = new();

            var query = context.Dishes.AsQueryable();
            if (!string.IsNullOrWhiteSpace(request.SearchTerm))
            {
                query = query.Where(x => x.Name.Contains(request.SearchTerm) || x.Category.Contains(request.SearchTerm));
            }
            if (request.OnlyActiveDishes)
            {
                query = query.Where(x => x.IsEnabled);
            }

            response.TotalAmount = query.Count();

            query = query.OrderBy(x => x.Name);
            query = query.Skip(request.Amount * request.Page);
            query = query.Take(request.Amount);

            response.Dishes = await query.Select(x => new DishDto.Index
            {
                Id = x.Id,
                Name = x.Name,
                Category = x.Category
            }).ToListAsync();

            return response;
        }
    }
}
