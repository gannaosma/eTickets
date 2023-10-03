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
        public async Task<List<Order>> GetOrdersByUserIDAsync(string userID)
		{
			return await _context.Orders.Include(n => n.OrderItems).ThenInclude(n => n.Movie).Where(n => n.UserID == userID).ToListAsync();
			
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
