using Horeca.Shared.Data.Repositories;

namespace Horeca.Shared.Data
{
    public interface IUnitOfWork
    {
        IIngredientRepository Ingredients { get; }
        IUnitRepository Units { get; }

        IDishRepository Dishes { get; }
        IMenuRepository Menus { get; }
        IMenuCardRepository MenuCards { get; }

        IPermissionRepository PermissionRepository { get; }
        IUserPermissionRepository UserPermissions { get; }

        IRestaurantRepository Restaurants { get; }

        Task CommitAsync();
    }
}