using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public interface IRepository<T>
    {
        Task<IQueryable<T>> GetAll();
        Task<IQueryable<T>> GetByCondition(Expression<Func<T, bool>> expression);
        Task<T> GetById(int id);
        Task Create(T entity);
        Task Update(T entity);
        Task Delete(T entity);
        Task DeleteById(int id);
        Task<IQueryable<T>> GetByRange(int skip, int number);
    }
}
