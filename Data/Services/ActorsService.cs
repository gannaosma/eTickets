using eTickets.Models;
using Microsoft.EntityFrameworkCore;

namespace eTickets.Data.Services
{
    public class ActorsService:IActorsService
    {
        public readonly ApplicationDbContext _context;
        public ActorsService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Actor>> GetAllAsync() 
        {
           return await _context.Actors.ToListAsync();
        }
        public async Task<Actor> GetByIdAsync(int id)
        {
			return await _context.Actors.FirstOrDefaultAsync(i => i.ID == id);
        }
        public async Task AddAsync(Actor actor)
        {
            await _context.AddAsync(actor);
            await _context.SaveChangesAsync();
        }
        public async Task<Actor> UpdateAsync(Actor actor)
        {
            _context.Update(actor);
            await _context.SaveChangesAsync();
            return actor;
        }
        public async Task DeleteAsync(Actor actor)
        {
            _context.Remove(actor);
            await _context.SaveChangesAsync();
        }
    }
}
