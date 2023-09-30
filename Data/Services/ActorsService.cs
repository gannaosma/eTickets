using eTickets.Data.Base;
using eTickets.Models;
using Microsoft.EntityFrameworkCore;

namespace eTickets.Data.Services
{
    public class ActorsService: EntityRepository<Actor>, IActorsService
    {
       
        public ActorsService(ApplicationDbContext context): base(context)
        {
        }
        
    }
}
