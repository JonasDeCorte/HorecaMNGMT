using Horeca.Shared.Data.Entities;

namespace Horeca.Shared.Data.Repositories
{
    public interface IMenuCardRepository : IRepository<MenuCard>
    {
        MenuCard GetMenuCardIncludingDependencies(int id);
    }
}