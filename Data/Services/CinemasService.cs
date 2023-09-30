using eTickets.Data.Base;
using eTickets.Models;

namespace eTickets.Data.Services
{
	public class CinemasService : EntityBaseRepository<Cinema>, ICinemasService
	{
        public CinemasService(ApplicationDbContext _context):base(_context)
        {   
        }
    }
}
