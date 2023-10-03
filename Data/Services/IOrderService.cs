using eTickets.Data;
using eTickets.Models;

namespace eTickets.Data.Services
{
	public interface IOrderService
	{
		Task StoreOrderAsync(List<ShoppingCartItem> items,string userID,string userEmailAdress);
		Task<List<Order>> GetOrdersByUserIDAsync(string userID);
	}
}
