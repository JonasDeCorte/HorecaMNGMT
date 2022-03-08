using Horeca.Shared.Data.Entities;

namespace Horeca.Shared.Data.Repositories
{
    public interface IMenuRepository : IRepository<Menu>
    {
        Menu GetMenuIncludingDependencies(int id);
    }
}