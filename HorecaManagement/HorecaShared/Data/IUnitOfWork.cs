using HorecaAPI.Data.Repositories;

namespace HorecaShared.Data
{
    public interface IUnitOfWork
    {
        IIngredientRepository Ingredients { get; }

        Task CommitAsync();
    }
}