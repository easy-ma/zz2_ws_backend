using System.Collections.Generic;

namespace DAL.Repositories{
  public interface IGenericRepository<T> {
    IEnumerable<T> GetAll();
    T Get(int id);
    void Create(T entity);
    void Update(T entity);
    void Delete(T entity);
  }
}