using System.ComponentModel.DataAnnotations;

namespace eTickets.Data.DTOs
{
	public class ActorDTO
	{
		[Display(Name = "Profile Picture")]
		[Required(ErrorMessage = "Profile Picture is required")]
		public string ProfilePictureURL { get; set; }

		[Display(Name = "Name")]
		[Required(ErrorMessage = "Name is required")]
		[StringLength(50, MinimumLength = 3, ErrorMessage = "Name must be between 3 and 50 chars")]
		public string FullName { get; set; }

		[Display(Name = "Bio")]
		[Required(ErrorMessage = "Bio is required")]
		public string Bio { get; set; }
	}
}
