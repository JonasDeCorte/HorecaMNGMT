using Horeca.Infrastructure.Data.Repositories.Generic;
using Horeca.Shared.Data.Entities;
using Horeca.Shared.Data.Repositories;

namespace Horeca.Infrastructure.Data.Repositories
{
    public class DishRepository : Repository<Dish>, IDishRepository
    {
        public DishRepository(DatabaseContext context) : base(context)
        {
        }
    }
}