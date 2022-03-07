namespace Horeca.Shared.Data.Repositories
{
    public interface IRepository<T>
    {
        IEnumerable<T> GetAll();

        T Get(int id);

        void Add(T entity);

        void Update(T entity);

        void Delete(int id);

        int Count();
    }
}