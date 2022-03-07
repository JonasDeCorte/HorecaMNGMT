using Horeca.Infrastructure.Data.Repositories.Generic;
using Horeca.Shared.Data.Entities;
using Horeca.Shared.Data.Repositories;
using Horeca.Shared.Dtos.Dishes;
using Microsoft.EntityFrameworkCore;

namespace Horeca.Infrastructure.Data.Repositories
{
    public class DishRepository : Repository<Dish>, IDishRepository
    {
        private readonly DatabaseContext context;

        public DishRepository(DatabaseContext context) : base(context)
        {
            this.context = context;
        }

        public Dish GetDishIncludingDependencies(int id)
        {
            Console.WriteLine(context.ChangeTracker.DebugView.LongView);
            return context.Dishes.Include(x => x.Ingredients).ThenInclude(x => x.Unit).Where(x => x.Id.Equals(id)).FirstOrDefault();
        }

        public DishDtoDetail GetIncludingDependencies(int id)
        {
            return context.Dishes.Include(x => x.Ingredients).ThenInclude(x => x.Unit).Select(dish => new DishDtoDetail
            {
                Id = dish.Id,
                Name = dish.Name,
                Description = dish.Description,
                Category = dish.Category,
                DishType = dish.DishType,
                Ingredients = dish.Ingredients,
            }).Where(x => x.Id.Equals(id)).FirstOrDefault();
        }
    }
}