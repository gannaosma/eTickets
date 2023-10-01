using eTickets.Data;
using eTickets.Data.DTOs;
using eTickets.Data.Services;
using eTickets.Data.ViewModels;
using eTickets.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace eTickets.Controllers
{
	public class MoviesController : Controller
	{
		private readonly IMoviesService _service;
		public MoviesController(IMoviesService service)
		{
			_service = service;
		}
		public async Task<IActionResult> Index()
		{
			var movies = await _service.GetAllAsync(n => n.Cinema);
			return View(movies);
		}

		public async Task<IActionResult> Filter(string searchString)
		{
			var movies = await _service.GetAllAsync(n => n.Cinema);

			if(!string.IsNullOrEmpty(searchString))
			{
				var filteredResult = movies.Where(n => n.Name.Contains(searchString) || n.Description.Contains(searchString)).ToList();
				return View("Index", filteredResult);
			}

			return View("Index", movies);
		}

		public async Task<IActionResult> Details(int id)
		{
			var movie = await _service.GetMovieByIdAsync(id);

			if (movie == null)
			{
				return View("NotFound");
			}

			return View(movie);
		}

        public async Task<IActionResult> Create()
        {
			var movieDropDownData = await _service.GetMovieDropDownsValues();

			ViewBag.Cinemas = new SelectList(movieDropDownData.Cinemas, "ID", "Name");
			ViewBag.Producers = new SelectList(movieDropDownData.Producers, "ID", "FullName");
			ViewBag.Actors = new SelectList(movieDropDownData.Actors, "ID", "FullName");

            return View();
        }

		[HttpPost]
		public async Task<IActionResult> Create(MovieVM movieVM)
		{
			if (!ModelState.IsValid)
			{
                var movieDropDownData = await _service.GetMovieDropDownsValues();

                ViewBag.Cinemas = new SelectList(movieDropDownData.Cinemas, "ID", "Name");
                ViewBag.Producers = new SelectList(movieDropDownData.Producers, "ID", "FullName");
                ViewBag.Actors = new SelectList(movieDropDownData.Actors, "ID", "FullName");
            }

			await _service.AddMovieAsync(movieVM);

			return RedirectToAction(nameof(Index));
		}

        public async Task<IActionResult> Edit(int id)
        {
			var movie = await _service.GetMovieByIdAsync(id);

			if(movie == null)
				return View("NotFound");

			var response = new MovieVM()
			{
				ID = movie.ID,
				Name = movie.Name,
				Description = movie.Description,
				Price = movie.Price,
				StartDate = movie.StartDate,
				EndDate = movie.EndDate,
				ImageURL = movie.ImageURL,
				CinemaID = movie.CinemaID,
				ProdcerID = movie.ProdcerID,
				MovieCategory = movie.MovieCategory,
				ActorIDs = movie.Actors_Movies.Select(n => n.ActorID).ToList(),
			};

            var movieDropDownData = await _service.GetMovieDropDownsValues();

            ViewBag.Cinemas = new SelectList(movieDropDownData.Cinemas, "ID", "Name");
            ViewBag.Producers = new SelectList(movieDropDownData.Producers, "ID", "FullName");
            ViewBag.Actors = new SelectList(movieDropDownData.Actors, "ID", "FullName");

            return View(response);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, MovieVM movieVM)
        {
			if (id != movieVM.ID)
				return View("NotFound");

            if (!ModelState.IsValid)
            {
                var movieDropDownData = await _service.GetMovieDropDownsValues();

                ViewBag.Cinemas = new SelectList(movieDropDownData.Cinemas, "ID", "Name");
                ViewBag.Producers = new SelectList(movieDropDownData.Producers, "ID", "FullName");
                ViewBag.Actors = new SelectList(movieDropDownData.Actors, "ID", "FullName");
				return View(movieVM);
            }

            await _service.UpdateMovieAsync(movieVM);

            return RedirectToAction(nameof(Index));
        }
	}
}
