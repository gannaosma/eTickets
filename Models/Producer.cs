using System.ComponentModel.DataAnnotations;

namespace eTickets.Models
{
	public class Producer
	{
		[Key]
        public int ID { get; set; }

		[Display(Name = "Profile Picture")]
		public string ProfilePictureURL { get; set; }

		[Display(Name = "Name")]
		public string FullName { get; set; }

		[Display(Name = "Bio")]
		public string Bio { get; set; }

		//Relationship
		public List<Movie> Movies { get; set; }
	}
}
