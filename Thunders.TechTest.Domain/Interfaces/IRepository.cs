using System.Linq.Expressions;

namespace Thunders.TechTest.Domain.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task<T> GetAsync(Guid id);
        Task<IEnumerable<T>> GetAllAsync();
        Task AddAsync(T entity);
        void Update(T entity);
        void Remove(T entity);
        Task<IEnumerable<T>> SearchAsync(Expression<Func<T, bool>> predicate);
        Task<int> SaveChangesAsync();
    }
}
