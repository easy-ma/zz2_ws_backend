using System;
using System.Linq;
using System.Linq.Expressions;

namespace DAL.Repositories{
  public interface IRepository<T> {
    IQueryable<T> GetAll();
    IQueryable<T> GetByCondition(Expression<Func<T, bool>> expression);
    T GetById(int id);
    void Create(T entity);
    void Update(T entity);
    void Delete(T entity);
    void DeleteById(int id);
    IQueryable<T> GetByRange(int skip, int number);
  }
}