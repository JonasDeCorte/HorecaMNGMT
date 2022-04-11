using Horeca.Shared.Data.Entities;

namespace Horeca.Shared.Data.Repositories
{
    public interface IKitchenRepository : IRepository<Kitchen>
    {
        public Task<Kitchen> GetKitchenWithDependenciesByID(int kitchenId);
    }
}