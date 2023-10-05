using eTickets.Models;
using Microsoft.EntityFrameworkCore;

namespace eTickets.Data.Services
{
	public class OrdersService : IOrderService
	{
		private readonly ApplicationDbContext _context;
        public OrdersService(ApplicationDbContext context)
        {
			_context = context;
        }
        public async Task<List<Order>> GetOrdersByUserIDAndRoleAsync(string userID,string userRole)
		{
			var orders =  await _context.Orders.Include(n => n.OrderItems).ThenInclude(n => n.Movie).Include(n=>n.User).ToListAsync();

			if(userRole != "Admin")
			{
				orders =  orders.Where(n => n.UserID == userID).ToList();
			}
			return orders;
		}

		public async Task StoreOrderAsync(List<ShoppingCartItem> items, string userID, string userEmailAdress)
		{
			var order = new Order()
			{
				UserID = userID,
				Email = userEmailAdress
			};
			await _context.Orders.AddAsync(order);
			await _context.SaveChangesAsync();

			foreach(var item in  items)
			{
				var orderItem = new OrderItem()
				{
					Amount = item.Amount,
					MovieID = item.Movie.ID,
					OrderID = order.ID,
					Price = item.Movie.Price,
				};
				await _context.OrderItems.AddAsync(orderItem);
			}
			await _context.SaveChangesAsync();
		}
	}
}
