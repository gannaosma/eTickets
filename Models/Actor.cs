﻿using System.ComponentModel.DataAnnotations;

namespace eTickets.Models
{
	public class Actor
	{
        [Key]
        public int ID { get; set; }

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
        //relationship
        public List<Actor_Movie> Actors_Movies { get; set; }
    }
}
