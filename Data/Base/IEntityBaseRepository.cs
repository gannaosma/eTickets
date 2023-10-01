using eTickets.Models;
using System.Linq.Expressions;

namespace eTickets.Data.Base
{
	public interface IEntityBaseRepository<T> where T: class
	{
		Task<IEnumerable<T>> GetAllAsync();
		Task<IEnumerable<T>> GetAllAsync(params Expression<Func<T, object>>[] includeproperties);
		Task<T> GetByIdAsync(int id);
		Task AddAsync(T entity);
		Task<T> UpdateAsync(T entity);
		Task DeleteAsync(T entity);

	}
}
