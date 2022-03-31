using Horeca.Infrastructure.Data.Repositories.Generic;
using Horeca.Shared.Data.Entities;
using Horeca.Shared.Data.Repositories;
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
            return context.Dishes.Include(x => x.DishIngredients).ThenInclude(x => x.Ingredient).ThenInclude(x => x.Unit).Where(x => x.Id.Equals(id)).FirstOrDefault();
        }
    }
}