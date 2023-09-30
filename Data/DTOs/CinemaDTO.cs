using System.ComponentModel.DataAnnotations;

namespace eTickets.Data.DTOs
{
	public class CinemaDTO
	{
		[Display(Name = "Cinema Logo")]
		[Required(ErrorMessage = "Cinema Logo is Required")]
		public string Logo { get; set; }

		[Display(Name = "Cinema NAme")]
		[Required(ErrorMessage = "Cinema name is Required")]
		public string Name { get; set; }
		[Display(Name = "Cinema Description")]
		[Required(ErrorMessage = "Cinema Description is Required")]
		public string Description { get; set; }
	}
}
