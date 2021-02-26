using System;
using System.Linq;
using System.Linq.Expressions;
using DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories {
  public class Repository<T> : IRepository<T> where T : BaseModel
  {

    protected readonly TurradgiverContext _context;  
    protected DbSet<T> entities;  
    public Repository(TurradgiverContext context) {  
      _context = context;  
      entities = context.Set<T>();  
    }  
    public void Create(T entity)
    {
      if (entity == null) {  
        throw new ArgumentNullException("[Add]: null entity");  
      }  
      entities.Add(entity);
      _context.SaveChanges();
    }

    public void Delete(T entity)
    {
      if (entity == null) {  
        throw new ArgumentNullException("[Remove]: null entity");  
      }  
      entities.Remove(entity);  
      _context.SaveChanges();
    }

    public void DeleteById(int id)
    { 
      T entity = GetById(id);
      entities.Remove(entity);  
      _context.SaveChanges();
    }

    public T GetById(int id)
    {
      return entities.FirstOrDefault(entity => entity.Id == id);
    }

    public IQueryable<T> GetAll()
    {
      return entities.ToList().AsQueryable();
    }

    public IQueryable<T> GetByRange(int skip, int number)
    {
      return entities.Skip(skip).Take(number);
    }


    public IQueryable<T> GetByCondition(Expression<Func<T, bool>> expression){
        // Doc about AsNoTracking 
        //https://entityframeworkcore.com/querying-data-asnotracking
        //https://entityframeworkcore.com/draft-querying-data-asnotracking
        return entities.Where(expression).AsNoTracking();
    }

    public void Update(T entity)
    {
      if (entity == null) {  
        throw new ArgumentNullException("[Update]: null entity");  
      }  
      entities.Update(entity);
      _context.SaveChanges();
    }
  }
}