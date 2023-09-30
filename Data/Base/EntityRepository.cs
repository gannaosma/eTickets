using eTickets.Models;
using Microsoft.EntityFrameworkCore;

namespace eTickets.Data.Base
{
	public class EntityRepository<T> : IEntityBaseRepository<T> where T : class
	{
		private readonly ApplicationDbContext _context;
        public EntityRepository(ApplicationDbContext context)
        {
			_context = context;
        }

		public async Task<IEnumerable<T>> GetAllAsync()
		{
			return await _context.Set<T>().ToListAsync();
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
