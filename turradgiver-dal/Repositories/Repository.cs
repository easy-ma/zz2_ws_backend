using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using turradgiver_dal.Models;

namespace turradgiver_dal.Repositories
{
    /// <summary>
    /// Repository for CRUD ops on the Database
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Repository<T> : IRepository<T> where T : BaseModel
    {

        private readonly TurradgiverContext _context;
        private DbSet<T> _entities;
        public Repository(TurradgiverContext context)
        {
            _context = context;
            _entities = context.Set<T>();
        }

        /// <summary>
        /// Create an item in the DB
        /// </summary>
        /// <param name="entity">The entity to add in the DB</param>
        /// <returns></returns>
        public async Task CreateAsync(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("[Add]: null entity");
            }
            await Task.Run(() => _entities.Add(entity));
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Delete an item from the DB
        /// </summary>
        /// <param name="entity">The entity to delete from the DB</param>
        /// <returns></returns>
        public async Task DeleteAsync(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("[Remove]: null entity");
            }
            await Task.Run(() => _entities.Remove(entity));
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Delete an item from the DB with an ID
        /// </summary>
        /// <param name="id">The id of the entity to delete</param>
        /// <returns></returns>
        public async Task DeleteByIdAsync(Guid id)
        {
            T entity = await GetByIdAsync(id);
            await DeleteAsync(entity);
        }

        /// <summary>
        /// Get an item from the db with an ID
        /// </summary>
        /// <param name="id">The id of the entity to get</param>
        /// <returns>The entity that wasfound</returns>
        public async Task<T> GetByIdAsync(Guid id)
        {
            return await Task.Run(() => _entities.FirstOrDefault(entity => entity.Id == id));
        }
        /// <summary>
        /// Get all elements from the DB
        /// </summary>
        /// <returns>The entities that were found</returns>
        public async Task<IQueryable<T>> GetAllAsync()
        {
            return (await _entities.ToListAsync()).AsQueryable();
        }

        /// <summary>
        /// Get all elements from the DB within a range
        /// </summary>
        /// <param name="skip">The starting index of entities to get</param>
        /// <param name="number">The number of entities to get</param>
        /// <returns>The entities that were found</returns>
        public async Task<IQueryable<T>> GetByRangeAsync(int skip, int number)
        {
            return await Task.Run(() => _entities.OrderByDescending(x => x.CreatedDate).Skip(skip).Take(number));
        }

        /// <summary>
        /// Get all elements from the DB within a range that meet a specific condition
        /// </summary>
        /// <param name="skip">The starting index of entities to get</param>
        /// <param name="number">The number of entities to get</param>
        /// <param name="expression">The expression to test the entities against</param>
        /// <returns>The entities that were found</returns>
        public async Task<IQueryable<T>> GetByRangeAsync(int skip, int number, Expression<Func<T, bool>> expression)
        {
            return await Task.Run(() => _entities.Where(expression).OrderByDescending(x => x.CreatedDate).Skip(skip).Take(number).AsNoTracking());
        }

        /// <summary>
        /// Get all elements from the DB that meet a specific condition
        /// </summary>
        /// <param name="expression">The expression to test the entities against</param>
        /// <returns>The entities that were found</returns>
        public async Task<IQueryable<T>> GetByConditionAsync(Expression<Func<T, bool>> expression)
        {
            // Doc about AsNoTracking 
            //https://entityframeworkcore.com/querying-data-asnotracking
            //https://entityframeworkcore.com/draft-querying-data-asnotracking
            return await Task.Run(() => _entities.Where(expression).AsNoTracking());
        }

        /// <summary>
        /// Update an item in the DB
        /// </summary>
        /// <param name="entity">The entity to update</param>
        /// <returns></returns>
        public async Task UpdateAsync(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("[Update]: null entity");
            }
            await Task.Run(() => _entities.Update(entity));
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Include the linked entity from the id foreign key
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public async Task<IQueryable<T>> IncludeAsync(Expression<Func<T, object>> expression)
        {
            return await Task.Run(() => _entities.Include(expression));
        }
    }
}
