using eTickets.Data.Enum;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eTickets.Models
{
    public class Movie
	{
		[Key]
        public int ID { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public DateTime StartDate { get; set;}
		public DateTime EndDate { get; set;}
        public double Price { get; set; }
		public string ImageURL { get; set; }
		public MovieCategory MovieCategory { get; set; }

		//relationship
		public List<Actor_Movie> Actors_Movies { get; set; }
		//cinema
		public int CinemaID { get; set; }
		[ForeignKey("CinemaID")]
		public Cinema Cinema { get; set; }

		//Prodcer
		public int ProdcerID { get; set; }
		[ForeignKey("ProdcerID")]
		public Producer Producer { get; set; }

	}
}
