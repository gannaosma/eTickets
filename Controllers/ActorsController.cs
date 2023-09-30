using eTickets.Data;
using eTickets.Data.DTOs;
using eTickets.Data.Services;
using eTickets.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace eTickets.Controllers
{
	public class ActorsController : Controller
	{
		private readonly IActorsService _service;
        public ActorsController(IActorsService service)
        {
			_service = service;
        }

        public async Task<IActionResult> Index()
		{
			var Actors = await _service.GetAllAsync();
			return View(Actors);
		}

        public IActionResult Create()
        {
            return View();
        }

		[HttpPost]
		public IActionResult Create(ActorDTO actordto)
		{
			if(!ModelState.IsValid)
			{
				return View(actordto);
			}

			var actor = new Actor()
			{
				ProfilePictureURL = actordto.ProfilePictureURL,
				FullName = actordto.FullName,
				Bio = actordto.Bio
			};
			_service.AddAsync(actor);
			return RedirectToAction(nameof(Index));
		}

		public async Task<IActionResult> Details(int id)
		{
			var actor = await _service.GetByIdAsync(id);

			if(actor == null)
			{
				return View("NotFound");
			}
			return View(actor);
		}

		public async Task<IActionResult> Edit(int id)
		{
			if(id  <= 0)
				return View("NotFound");

			var actor = await _service.GetByIdAsync(id);

			if (actor == null)
				return View("NotFound");

			return View(actor);
		}

		[HttpPost]
		public async Task<IActionResult> Edit([FromRoute]int id,ActorDTO actordto)
		{
			if (id <= 0)
				return View("NotFound");

			if (!ModelState.IsValid)
				return View(actordto);

			var actor = new Actor
			{
				ID = id,
				ProfilePictureURL = actordto.ProfilePictureURL,
				FullName = actordto.FullName,
				Bio = actordto.Bio
			};
			await  _service.UpdateAsync(actor);
			return RedirectToAction(nameof(Index));
		}

		public async Task<IActionResult> Delete(int id)
		{
			if (id <= 0)
				return View("NotFound");

			var actor = await _service.GetByIdAsync(id);

			if (actor == null)
				return View("NotFound");

			return View(actor);
		}

		[HttpPost, ActionName("Delete")]
		public async Task<IActionResult> DeleteConfirmed([FromRoute] int id)
		{
			if (id <= 0)
				return View("NotFound");

			var actor = await _service.GetByIdAsync(id);

			if (actor == null)
				return View("NotFound");

			 await _service.DeleteAsync(actor);
			return RedirectToAction(nameof(Index));
		}
	}
}
