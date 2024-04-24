using System.ComponentModel.DataAnnotations;

namespace ModernMoviesWeb.Pages.Model
{
	public class Movie
	{
		[Display(Name = "Movie ID")]
		public int MovieID { get; set; }
		[Required]
		[Display(Name = "Movie Name")]
		public string MovieName { get; set; }
		[Display(Name = "Movie Description")]
		public string MovieDesc { get; set;}
		[Display(Name = "Movie Runtime (Minutes)")]
		[Required]
		public int MinRuntime { get; set; }
		
		[Required]
		public int RatingID { get; set; }
		
		[Required]
		public int GenreID { get; set; }

		[Required]
		[Display(Name = "Image Link")]
		public string Image {  get; set; }

		[Required]
		[Display(Name = "Release Date")]
		public DateTime ReleaseDate { get; set; }
		
	}
}
