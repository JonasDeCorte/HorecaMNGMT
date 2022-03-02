using Horeca.Infrastructure.Data.Repositories;
using Horeca.Shared.Data;
using Horeca.Shared.Data.Repositories;

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

        public IUnitRepository Units => new UnitRepository(_context);

        public IDishRepository Dishes => new DishRepository(_context);

        public async Task CommitAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}