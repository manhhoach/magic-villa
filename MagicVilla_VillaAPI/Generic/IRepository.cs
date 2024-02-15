using System.Linq.Expressions;

namespace MagicVilla_VillaAPI.Generic
{
    public interface IRepository<T> where T : class
    {
        Task Create(T entity);

        Task Remove(T entity);

        Task Save();

        Task<List<T>> GetAll(Expression<Func<T, bool>>? filter = null, string? includeProperties = null );

        Task<T> GetOne(Expression<Func<T, bool>>? filter = null, bool tracked = true, string? includeProperties = null);
    }
}
