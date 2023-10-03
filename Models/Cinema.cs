using System.ComponentModel.DataAnnotations;

namespace eTickets.Models
{
	public class Cinema
	{
        public int ID { get; set; }

		public string Logo { get; set; }

		public string Name { get; set; }

		public string Description { get; set; }

		//relationship
		public List<Movie> Movies { get; set; }
	}
}
