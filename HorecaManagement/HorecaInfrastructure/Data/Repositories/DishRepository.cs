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
            Console.WriteLine(context.ChangeTracker.DebugView.LongView);
            return context.Dishes.Include(x => x.Ingredients).ThenInclude(x => x.Unit).Where(x => x.Id.Equals(id)).FirstOrDefault();
        }
    }
}