using System.ComponentModel.DataAnnotations;

namespace ModernMoviesWeb.Pages.Model
{
	public class Movie
	{

		// Unique identifier for the movie, not required to be input by the user.
		[Display(Name = "Movie ID")]
		public int MovieID { get; set; }

		// The name of the movie, required field.
		[Required]
		[Display(Name = "Movie Name")]
		public string MovieName { get; set; }

		// A short description of the movie.
		[Display(Name = "Movie Description")]
		public string MovieDesc { get; set;}

		// The runtime of the movie in minutes, required field.
		[Display(Name = "Movie Runtime (Minutes)")]
		[Required]
		public int MinRuntime { get; set; }

		// The rating associated with this movie, required field.
		[Required]
		public int RatingID { get; set; }

		// The genre associated with this movie, required field.
		[Required]
		public int GenreID { get; set; }

		// A link to the movie's poster image, required field.
		[Required]
		[Display(Name = "Image Link")]
		public string Image {  get; set; }

		// The release date of the movie, required field.
		[Required]
		[Display(Name = "Release Date")]
		public DateTime ReleaseDate { get; set; }
		
	}
}
