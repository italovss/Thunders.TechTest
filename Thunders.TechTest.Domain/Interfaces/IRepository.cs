using System.Linq.Expressions;

namespace Thunders.TechTest.Domain.Interfaces
{
    public interface IRepository<T> where T : class
    {
        IQueryable<T> Query();
        Task AddAsync(T entity);
        Task<int> SaveChangesAsync();
    }
}
