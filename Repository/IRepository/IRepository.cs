using System.Linq.Expressions;

namespace ToDoService.Repository.IRepository
{
    public interface IRepository<T> where T : class
    {
        Task CreateAsync(T entity);
        Task RemoveAsync(T entity);
        Task SaveAsync();
        Task UpdateAsync(T entity);
        Task<List<T>> GetAllAsync(Expression<Func<T, bool>>? filter = null, string includeProperties = null);
        Task<T> GetAsync(Expression<Func<T, bool>>? filter = null, bool tracked = true, string includeProperties = null);
    }
}
