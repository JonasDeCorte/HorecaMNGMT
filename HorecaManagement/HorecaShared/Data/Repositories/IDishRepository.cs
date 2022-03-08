using Horeca.Shared.Data.Entities;

namespace Horeca.Shared.Data.Repositories
{
    public interface IDishRepository : IRepository<Dish>
    {
        Dish GetDishIncludingDependencies(int id);
    }
}