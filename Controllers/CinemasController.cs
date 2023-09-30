using eTickets.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace eTickets.Controllers
{
	public class CinemasController : Controller
	{
		private readonly ApplicationDbContext _context;
		public CinemasController(ApplicationDbContext context)
		{
			_context = context;
		}
		public async Task<IActionResult> Index()
		{
			var cinemas = await _context.Cinemas.ToListAsync();
			return View(cinemas);
		}
	}
}
