using System;
using System.Linq;
using System.Linq.Expressions;
using DAL.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class Repository<T> : IRepository<T> where T : BaseModel
    {

        private readonly TurradgiverContext _context;
        private DbSet<T> _entities;
        public Repository(TurradgiverContext context)
        {
            _context = context;
            _entities = context.Set<T>();
        }
        public async Task Create(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("[Add]: null entity");
            }
            await Task.Run(() => _entities.Add(entity));
            await _context.SaveChangesAsync();
        }

        public async Task Delete(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("[Remove]: null entity");
            }
            await Task.Run(() => _entities.Remove(entity));
            await _context.SaveChangesAsync();
        }

        public async Task DeleteById(int id)
        {
            T entity = await GetById(id);
            await Delete(entity);
        }

        public async Task<T> GetById(int id)
        {
            return await Task.Run(() =>_entities.FirstOrDefault(entity => entity.Id == id));
        }

        public async Task<IQueryable<T>> GetAll()
        {
            return (await _entities.ToListAsync()).AsQueryable();
        }

        public async Task<IQueryable<T>> GetByRange(int skip, int number)
        {
            return await Task.Run(() => _entities.Skip(skip).Take(number));
        }


        public async Task<IQueryable<T>> GetByCondition(Expression<Func<T, bool>> expression)
        {
            // Doc about AsNoTracking 
            //https://entityframeworkcore.com/querying-data-asnotracking
            //https://entityframeworkcore.com/draft-querying-data-asnotracking
            return await Task.Run(() => _entities.Where(expression).AsNoTracking());
        }

        public async Task Update(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("[Update]: null entity");
            }
            await Task.Run(() =>_entities.Update(entity));
            await _context.SaveChangesAsync();
        }
    }
}
