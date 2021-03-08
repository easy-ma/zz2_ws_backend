using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public interface IRepository<T>
    {
        Task<IQueryable<T>> GetAllAsync();
        Task<IQueryable<T>> GetByConditionAsync(Expression<Func<T, bool>> expression);
        Task<T> GetByIdAsync(Guid id);
        Task CreateAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
        Task DeleteByIdAsync(Guid id);
        Task<IQueryable<T>> GetByRangeAsync(int skip, int number);
        Task<IQueryable<T>> GetByRangeAsync(int skip, int number, Expression<Func<T, bool>> include);

        Task<IQueryable<T>> IncludeAsync(Expression<Func<T,object>> include);
    }
}
