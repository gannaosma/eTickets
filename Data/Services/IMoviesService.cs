using eTickets.Data.Base;
using eTickets.Data.ViewModels;
using eTickets.Models;

namespace eTickets.Data.Services
{
	public interface IMoviesService : IEntityBaseRepository<Movie>
	{
        Task<Movie> GetMovieByIdAsync(int id);
		Task<MovieDropDownsVM> GetMovieDropDownsValues();
		Task AddMovieAsync(MovieVM movieVM);
		Task UpdateMovieAsync(MovieVM movieVM);

	}
}
