using eTickets.Models;
using Microsoft.EntityFrameworkCore;

namespace eTickets.Data.Cart
{
	public class ShoppingCart
	{
		public ApplicationDbContext _context;
		public string shoppingCartID;
		public List<ShoppingCartItem> ShoppingCartItems;

		public ShoppingCart(ApplicationDbContext context)
		{
			_context = context;
		}

		public static ShoppingCart GetShoppingCart(IServiceProvider services)
		{
			ISession session = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;

			var context = services.GetService<ApplicationDbContext>();

			string CartID = session.GetString("CartID") ?? Guid.NewGuid().ToString();

			session.SetString("CartID",  CartID);

			return new ShoppingCart(context) { shoppingCartID = CartID};
		}

		public void AddItemToCart(Movie movie)
		{
			var shoppingCartItem = _context.ShoppingCartItems.FirstOrDefault(n => n.Movie.ID == movie.ID
			&& n.ShoppingCartID == shoppingCartID);

			if (shoppingCartItem == null)
			{
				shoppingCartItem = new ShoppingCartItem()
				{
					ShoppingCartID = shoppingCartID,
					Movie = movie,
					Amount = 1,
				};
				_context.ShoppingCartItems.Add(shoppingCartItem);
			}
			else
			{
				shoppingCartItem.Amount++;
			}
			_context.SaveChanges();
		}

		public void RemoveItemFromCart(Movie movie)
		{
			var shoppingCartItem = _context.ShoppingCartItems.FirstOrDefault(n => n.Movie.ID == movie.ID
			&& n.ShoppingCartID == shoppingCartID);

			if (shoppingCartItem != null)
			{
				if (shoppingCartItem.Amount > 1)
				{
					shoppingCartItem.Amount--;
				}
				else
				{
					_context.ShoppingCartItems.Remove(shoppingCartItem);
				}
			}
			_context.SaveChanges();
		}

		public List<ShoppingCartItem> GetShoppingCartItems()
		{
			return ShoppingCartItems ?? (ShoppingCartItems = _context.ShoppingCartItems
				.Where(n => n.ShoppingCartID == shoppingCartID)
				.Include(m => m.Movie).ToList());
		}

		public double GetShoppingCartTotal()
		{
			return _context.ShoppingCartItems.Where(i => i.ShoppingCartID == shoppingCartID)
				.Select(m => m.Movie.Price * m.Amount).Sum();
		}

		public async Task ClearShoppingCartAsync()
		{
			var items = await _context.ShoppingCartItems.Where(i => i.ShoppingCartID == shoppingCartID).ToListAsync();
				
			_context.ShoppingCartItems.RemoveRange(items);
			await _context.SaveChangesAsync(); 
		}
	}
}
