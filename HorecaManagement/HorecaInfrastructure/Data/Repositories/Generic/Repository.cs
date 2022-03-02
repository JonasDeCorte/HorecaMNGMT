using Horeca.Shared.Data.Entities;
using Horeca.Shared.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Horeca.Infrastructure.Data.Repositories.Generic
{
    public class Repository<T> : IRepository<T> where T : class
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
            _dbSet.Add(entity);
        }

        public int Count()
        {
            return _dbSet.Count();
        }

        public void Delete(object id)
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

        public T Get(object id)
        {
            var x = _dbSet.Find(id);
            return x;
        }

        public IEnumerable<T> GetAll()
        {
            IQueryable<T> query = _context.Set<T>();
            Type type = typeof(T);

            foreach (var field in type.GetFields(BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly))
            {
                if (!field.FieldType.Namespace.StartsWith("System"))
                {
                    query = query.Include(field.FieldType.Name);
                }
            }
            return query.AsEnumerable();
        }

        public void Update(T entity)
        {
            _dbSet.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }
    }
}