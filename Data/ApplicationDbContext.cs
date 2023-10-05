using eTickets.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace eTickets.Data
{
	public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
	{
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) :base(options)
        {
            
        }
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Actor_Movie>().HasKey(am => new
			{
				am.MovieID,
				am.ActorID
			});
			modelBuilder.Entity<Actor_Movie>().HasOne(m => m.Movie).WithMany(am => am.Actors_Movies).HasForeignKey(m => m.MovieID);
			modelBuilder.Entity<Actor_Movie>().HasOne(m => m.Actor).WithMany(am => am.Actors_Movies).HasForeignKey(m => m.ActorID);
			base.OnModelCreating(modelBuilder);
		}
		public DbSet<Actor> Actors { get; set; }
		public DbSet<Movie> Movies { get; set; }
		public DbSet<Actor_Movie> Actors_Movies { get; set; }
		public DbSet<Cinema> Cinemas { get; set; }
		public DbSet<Producer> Producers { get; set; }
		public DbSet<Order> Orders { get; set; }
		public DbSet<OrderItem> OrderItems { get; set; }
		public DbSet<ShoppingCartItem> ShoppingCartItems { get; set; }
	}
}
