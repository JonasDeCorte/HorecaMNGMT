using Horeca.Shared.Data.Repositories;

namespace Horeca.Shared.Data
{
    public interface IUnitOfWork
    {
        IIngredientRepository Ingredients { get; }
        IUnitRepository Units { get; }

        Task CommitAsync();
    }
}