using eTickets.Data.Base;
using eTickets.Models;

namespace eTickets.Data.Services
{
	public class ProducersService : EntityRepository<Producer>, IProducersService
	{
        public ProducersService(ApplicationDbContext context):base(context)
        {
            
        }
    }
}
