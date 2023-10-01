using eTickets.Data.Base;
using eTickets.Data.ViewModels;
using eTickets.Models;
using Microsoft.EntityFrameworkCore;

namespace eTickets.Data.Services
{
	public class MoviesService:EntityBaseRepository<Movie>, IMoviesService
	{
		private readonly ApplicationDbContext _context;

		public MoviesService(ApplicationDbContext context):base(context)
        {
			_context = context;
        }

		public async Task<Movie> GetMovieByIdAsync(int id)
		{
			var movie =await _context.Movies
				.Include(c => c.Cinema)
				.Include(p => p.Producer)
				.Include(am => am.Actors_Movies)
				.ThenInclude(a => a.Actor)
				.FirstOrDefaultAsync(i => i.ID == id);
			return movie;
		}
		public async Task<MovieDropDownsVM> GetMovieDropDownsValues()
		{
			var response = new MovieDropDownsVM();
			response.Actors = await _context.Actors.OrderBy(f => f.FullName).ToListAsync();
			response.Producers = await _context.Producers.OrderBy(f => f.FullName).ToListAsync();
			response.Cinemas = await _context.Cinemas.OrderBy(f => f.Name).ToListAsync();
			return response;
		}

		public async Task AddMovieAsync(MovieVM movieVM)
		{
			var movie = new Movie()
			{
				Name = movieVM.Name,
				Description = movieVM.Description,
				Price = movieVM.Price,
				StartDate = movieVM.StartDate,
				EndDate = movieVM.EndDate,
				ImageURL = movieVM.ImageURL,
				CinemaID = movieVM.CinemaID,
				ProdcerID = movieVM.ProdcerID,
				MovieCategory = movieVM.MovieCategory,
			};

			await _context.Movies.AddAsync(movie);
			await _context.SaveChangesAsync();

			foreach (var actorID in movieVM.ActorIDs)
			{
				var actorMovie = new Actor_Movie()
				{
					MovieID = movie.ID,
					ActorID = actorID
				};
				await _context.Actors_Movies.AddAsync(actorMovie);
			}
			await _context.SaveChangesAsync();
		}

        public async Task UpdateMovieAsync(MovieVM movieVM)
        {
			var dbmovie = await _context.Movies.FirstOrDefaultAsync(i => i.ID == movieVM.ID);

			if(dbmovie != null)
			{
				dbmovie.Name = movieVM.Name;
				dbmovie.Description = movieVM.Description;
                dbmovie.Price = movieVM.Price;
				dbmovie.StartDate = movieVM.StartDate;
				dbmovie.EndDate = movieVM.EndDate;
				dbmovie.ImageURL = movieVM.ImageURL;
				dbmovie.CinemaID = movieVM.CinemaID;
				dbmovie.ProdcerID = movieVM.ProdcerID;
				dbmovie.MovieCategory = movieVM.MovieCategory;

                await _context.SaveChangesAsync();
            }

			var existingActor = await _context.Actors_Movies.Where(i=> i.MovieID ==  movieVM.ID).ToListAsync();
			_context.Actors_Movies.RemoveRange(existingActor);
			await _context.SaveChangesAsync();

            foreach (var actorID in movieVM.ActorIDs)
            {
                var actorMovie = new Actor_Movie()
                {
                    MovieID = movieVM.ID,
                    ActorID = actorID
                };
                await _context.Actors_Movies.AddAsync(actorMovie);
            }
            await _context.SaveChangesAsync();
        }


    }
}
