using eTickets.Data;
using eTickets.Data.DTOs;
using eTickets.Data.Services;
using eTickets.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace eTickets.Controllers
{
	public class ProducersController : Controller
	{
		private readonly IProducersService _service;
		public ProducersController(IProducersService service)
		{
			_service = service;
		}
		public async Task<IActionResult> Index()
		{
			var producers = await _service.GetAllAsync();
			return View(producers);
		}

		public async Task<IActionResult> Details(int id)
		{
			var producer = await _service.GetByIdAsync(id);

			if (producer == null)
			{
				return View("NotFound");
			}
			return View(producer);
		}

		public IActionResult Create()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Create(ProducerDTO producerdto)
		{
			if (!ModelState.IsValid)
			{
				return View(producerdto);
			}

			var producer = new Producer()
			{
				ProfilePictureURL = producerdto.ProfilePictureURL,
				FullName = producerdto.FullName,
				Bio = producerdto.Bio
			};
			await _service.AddAsync(producer);
			return RedirectToAction(nameof(Index));
		}

		public async Task<IActionResult> Edit(int id)
		{
			if (id <= 0)
				return View("NotFound");

			var producer = await _service.GetByIdAsync(id);

			if (producer == null)
				return View("NotFound");

			return View(producer);
		}

		[HttpPost]
		public async Task<IActionResult> Edit([FromRoute] int id, ProducerDTO producerdto)
		{
			if (id <= 0)
				return View("NotFound");

			if (!ModelState.IsValid)
				return View(producerdto);

			var producer = new Producer
			{
				ID = id,
				ProfilePictureURL = producerdto.ProfilePictureURL,
				FullName = producerdto.FullName,
				Bio = producerdto.Bio
			};
			await _service.UpdateAsync(producer);
			return RedirectToAction(nameof(Index));
		}

		public async Task<IActionResult> Delete(int id)
		{
			if (id <= 0)
				return View("NotFound");

			var producer = await _service.GetByIdAsync(id);

			if (producer == null)
				return View("NotFound");

			return View(producer);
		}

		[HttpPost, ActionName("Delete")]
		public async Task<IActionResult> DeleteConfirmed([FromRoute] int id)
		{
			if (id <= 0)
				return View("NotFound");

			var producer = await _service.GetByIdAsync(id);

			if (producer == null)
				return View("NotFound");

			await _service.DeleteAsync(producer);
			return RedirectToAction(nameof(Index));
		}
	}
}
