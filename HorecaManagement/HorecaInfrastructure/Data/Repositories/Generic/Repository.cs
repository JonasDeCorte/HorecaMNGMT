using Horeca.Shared.Data;
using Horeca.Shared.Data.Repositories;

using Microsoft.EntityFrameworkCore;

namespace Horeca.Infrastructure.Data.Repositories.Generic
{
    public class Repository<T> : IRepository<T> where T : class, IBaseEntityId
    {
        private readonly DatabaseContext _context;
        private readonly DbSet<T> _dbSet;

        public Repository(DatabaseContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public void Add(T entity)
        {
            Console.WriteLine(_context.ChangeTracker.DebugView.ShortView);

            _dbSet.Add(entity);
        }

        public int Count()
        {
            return _dbSet.Count();
        }

        public void Delete(int id)
        {
            var entity = Get(id);
            if (entity != null)
            {
                if (_context.Entry(entity).State == EntityState.Detached)
                {
                    _dbSet.Attach(entity);
                }
                _dbSet.Remove(entity);
            }
        }

        public T Get(int id)
        {
            Console.WriteLine(_context.ChangeTracker.DebugView.ShortView);

            return _dbSet.Find(id);
        }

        public IEnumerable<T> GetAll()
        {
            Console.WriteLine(_context.ChangeTracker.DebugView.ShortView);

            return _dbSet.AsEnumerable();
        }

        public void Update(T entity)
        {
            _dbSet.Attach(entity);
            Console.WriteLine(_context.ChangeTracker.DebugView.ShortView);
            _context.Entry(entity).State = EntityState.Modified;
        }
    }
}