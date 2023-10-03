using System.ComponentModel.DataAnnotations;

namespace eTickets.Models
{
	public class Order
	{
		[Key]
        public int ID { get; set; }
        public string Email { get; set; }
        public string UserID { get; set; }

        public List<OrderItem> OrderItems { get; set; }
    }
}
