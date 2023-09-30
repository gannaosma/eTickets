using eTickets.Data;
using eTickets.Data.DTOs;
using eTickets.Data.Services;
using eTickets.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace eTickets.Controllers
{
	public class CinemasController : Controller
	{
		private readonly ICinemasService _service;
		public CinemasController(ICinemasService service)
		{
			_service = service;
		}
		public async Task<IActionResult> Index()
		{
			var cinemas = await _service.GetAllAsync();
			return View(cinemas);
		}
		public IActionResult Create()
		{
			return View();
		}

		[HttpPost]
		public IActionResult Create(CinemaDTO cinemadto)
		{
			if (!ModelState.IsValid)
			{
				return View(cinemadto);
			}

			var cinema = new Cinema()
			{
				Logo = cinemadto.Logo,
				Name = cinemadto.Name,
				Description = cinemadto.Description
			};
			_service.AddAsync(cinema);
			return RedirectToAction(nameof(Index));
		}
		public async Task<IActionResult> Details(int id)
		{
			var cinema = await _service.GetByIdAsync(id);

			if (cinema == null)
			{
				return View("NotFound");
			}
			return View(cinema);
		}

		public async Task<IActionResult> Edit(int id)
		{
			if (id <= 0)
				return View("NotFound");

			var cinema = await _service.GetByIdAsync(id);

			if (cinema == null)
				return View("NotFound");

			return View(cinema);
		}

		[HttpPost]
		public async Task<IActionResult> Edit([FromRoute] int id, CinemaDTO cinemadto)
		{
			if (id <= 0)
				return View("NotFound");

			if (!ModelState.IsValid)
				return View(cinemadto);

			var cinema = new Cinema
			{
				ID = id,
				Logo = cinemadto.Logo,
				Name = cinemadto.Name,
				Description = cinemadto.Description
			};
			await _service.UpdateAsync(cinema);
			return RedirectToAction(nameof(Index));
		}

		public async Task<IActionResult> Delete(int id)
		{
			if (id <= 0)
				return View("NotFound");

			var cinema = await _service.GetByIdAsync(id);

			if (cinema == null)
				return View("NotFound");

			return View(cinema);
		}

		[HttpPost, ActionName("Delete")]
		public async Task<IActionResult> DeleteConfirmed([FromRoute] int id)
		{
			if (id <= 0)
				return View("NotFound");

			var cinema = await _service.GetByIdAsync(id);

			if (cinema == null)
				return View("NotFound");

			await _service.DeleteAsync(cinema);
			return RedirectToAction(nameof(Index));
		}
	}
}
