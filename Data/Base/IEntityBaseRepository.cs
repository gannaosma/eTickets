using eTickets.Models;

namespace eTickets.Data.Base
{
	public interface IEntityBaseRepository<T> where T: class
	{
		Task<IEnumerable<T>> GetAllAsync();
		Task<T> GetByIdAsync(int id);
		Task AddAsync(T entity);
		Task<T> UpdateAsync(T entity);
		Task DeleteAsync(T entity);

	}
}
