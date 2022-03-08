using Horeca.Shared.Data.Entities;
using Horeca.Shared.Dtos.Menus;

namespace Horeca.Shared.Data.Repositories
{
    public interface IMenuRepository : IRepository<Menu>
    {
        MenuDetailDto GetMenuDtoDetailIncludingDependencies(int id);

        Menu GetMenuIncludingDependencies(int id);
    }
}