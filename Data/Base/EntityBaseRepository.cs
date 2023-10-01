using eTickets.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace eTickets.Data.Base
{
	public class EntityBaseRepository<T> : IEntityBaseRepository<T> where T : class
	{
		private readonly ApplicationDbContext _context;
        public EntityBaseRepository(ApplicationDbContext context)
        {
			_context = context;
        }

		public async Task<IEnumerable<T>> GetAllAsync()
		{
			return await _context.Set<T>().ToListAsync();
		}
		public async Task<IEnumerable<T>> GetAllAsync(params Expression<Func<T, object>>[] includeproperties)
		{
			IQueryable<T> query = _context.Set<T>();
			query = includeproperties.Aggregate(query, (current, includeproperties) => current.Include(includeproperties));
			return await query.ToListAsync();
		}


		public async Task<T> GetByIdAsync(int id)
		{
			return await _context.Set<T>().FindAsync(id);
		}

		public async Task AddAsync(T entity)
		{
			await _context.AddAsync(entity);
			await _context.SaveChangesAsync();
		}
		public async Task<T> UpdateAsync(T entity)
		{
			_context.Update(entity);
			await _context.SaveChangesAsync();
			return entity;
		}

		public async Task DeleteAsync(T entity)
		{
			_context.Remove(entity);
			await _context.SaveChangesAsync();
		}
	}
}
