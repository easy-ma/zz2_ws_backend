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
        public async Task CreateAsync(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("[Add]: null entity");
            }
            await Task.Run(() => _entities.Add(entity));
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("[Remove]: null entity");
            }
            await Task.Run(() => _entities.Remove(entity));
            await _context.SaveChangesAsync();
        }

        public async Task DeleteByIdAsync(Guid id)
        {
            T entity = await GetByIdAsync(id);
            await DeleteAsync(entity);
        }

        public async Task<T> GetByIdAsync(Guid id)
        {
            return await Task.Run(() =>_entities.FirstOrDefault(entity => entity.Id == id));
        }

        public async Task<IQueryable<T>> GetAllAsync()
        {
            return (await _entities.ToListAsync()).AsQueryable();
        }

        public async Task<IQueryable<T>> GetByRangeAsync(int skip, int number)
        {
            return await Task.Run(() => _entities.OrderByDescending(x => x.CreatedDate).Skip(skip).Take(number));
        }

        public async Task<IQueryable<T>> GetByRangeAsync(int skip, int number, Expression<Func<T, bool>> expression)
        {
            return await Task.Run(() => _entities.Where(expression).OrderByDescending(x => x.CreatedDate).Skip(skip).Take(number).AsNoTracking());
        }

        public async Task<IQueryable<T>> GetByConditionAsync(Expression<Func<T, bool>> expression)
        {
            // Doc about AsNoTracking 
            //https://entityframeworkcore.com/querying-data-asnotracking
            //https://entityframeworkcore.com/draft-querying-data-asnotracking
            return await Task.Run(() => _entities.Where(expression).AsNoTracking());
        }

        public async Task UpdateAsync(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("[Update]: null entity");
            }
            await Task.Run(() =>_entities.Update(entity));
            await _context.SaveChangesAsync();
        }

        public async Task<IQueryable<T>> IncludeAsync(Expression<Func<T,object>> expression){
            return await Task.Run(()=> _entities.Include(expression));
        }

    }
}
