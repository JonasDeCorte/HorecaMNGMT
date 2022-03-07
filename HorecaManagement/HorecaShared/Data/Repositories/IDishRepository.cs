using Horeca.Shared.Data.Entities;
using Horeca.Shared.Dtos.Dishes;

namespace Horeca.Shared.Data.Repositories
{
    public interface IDishRepository : IRepository<Dish>
    {
        DishDtoDetail GetIncludingDependencies(int id);
        // betere manier voor vinden? 
        Dish GetDishIncludingDependencies(int id);
    }
}