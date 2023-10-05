using eTickets.Data.Cart;
using eTickets.Data.Services;
using eTickets.Data.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace eTickets.Controllers
{
	public class OrdersController : Controller
	{
		private readonly IMoviesService _moviesService;
		private readonly IOrderService _orderService;
		private readonly ShoppingCart _shoppingCart;
        public OrdersController(IMoviesService moviesService, ShoppingCart shoppingCart, IOrderService orderService)
        {
			_moviesService = moviesService;
			_shoppingCart = shoppingCart;
			_orderService = orderService;
        }

		public async Task<IActionResult> Index()
		{
			string userID = User.FindFirstValue(ClaimTypes.NameIdentifier);
			string userRole = User.FindFirstValue(ClaimTypes.Role);

			var orders = await _orderService.GetOrdersByUserIDAndRoleAsync(userID,userRole);

			return View(orders);
		}


        public IActionResult ShoppingCart()
		{
			var items = _shoppingCart.GetShoppingCartItems();

			_shoppingCart.ShoppingCartItems = items;

			var response = new ShoppingCartVM()
			{
				ShoppingCart = _shoppingCart,
				ShoppingCartTotal = _shoppingCart.GetShoppingCartTotal(),
			};
			return View(response);
		}
		
		public async Task<IActionResult> AddItemToShoppingCart(int id)
		{
			var item = await _moviesService.GetMovieByIdAsync(id);

			if(item != null)
			{
				_shoppingCart.AddItemToCart(item);
			}
			return RedirectToAction(nameof(ShoppingCart));
		}

        public async Task<IActionResult> RemoveItemFromShoppingCart(int id)
        {
            var item = await _moviesService.GetMovieByIdAsync(id);

            if (item != null)
            {
                _shoppingCart.RemoveItemFromCart(item);
            }
            return RedirectToAction(nameof(ShoppingCart));
        }

		public async Task<IActionResult> CompleteOrder()
		{
			var items =  _shoppingCart.GetShoppingCartItems();

			string userID = User.FindFirstValue(ClaimTypes.NameIdentifier);
			string userEmailAdress = User.FindFirstValue(ClaimTypes.Email); ;

			await _orderService.StoreOrderAsync(items, userID, userEmailAdress);
			await _shoppingCart.ClearShoppingCartAsync();

			return View("OrderCompleted");
		}

	}
}
