using System.ComponentModel.DataAnnotations;

namespace eTickets.Models
{
	public class ShoppingCartItem
	{
		[Key]
        public int ID { get; set; }
        public Movie Movie { get; set; }
        public int Amount { get; set; }
        public string ShoppingCartID { get; set; }
    }
}
