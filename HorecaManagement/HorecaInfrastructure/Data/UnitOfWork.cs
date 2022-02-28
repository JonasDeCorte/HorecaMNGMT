using Horeca.Infrastructure.Data.Repositories;
using HorecaAPI.Data.Repositories;
using HorecaShared.Data;

namespace Horeca.Infrastructure.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DatabaseContext _context;

        public UnitOfWork(DatabaseContext context)
        {
            _context = context;
        }

        public IIngredientRepository Ingredients => new IngredientRepository(_context);

        public async Task CommitAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}