using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using ModernMoviesBusiness;
using ModernMoviesWeb.Pages.Model;

namespace ModernMoviesWeb.Pages.MoviePages
{
	[BindProperties]
	public class MoviesModel : PageModel
	{
		//list of genres and ratings pulled from database, as well as non-implemented attempts  at getting the checkboxed genres to store
		public List<GenreInfo> Genres { get; set; } = new List<GenreInfo>();
		public List<int> selectedGenreIDs { get; set; }
		public List<RatingInfo> Ratings { get; set; } = new List<RatingInfo>();
		public List<int> selectedRatingIDs { get; set; }
		public List<Movie> Movies { get; set; } = new List<Movie>();

		//populating list of genres, ratings, and the movies displayed on the page
		public void OnGet()
		{
			PopulateGenreList();
			PopulateRatingList();
			PopulateMovie();
		}

		//populates the list of genres, similar to how is done in the Add Movie script
		private void PopulateGenreList()
		{
			using (SqlConnection conn = new SqlConnection(SecurityHelper.GetDBConnectionString()))
			{
				string cmdText = "SELECT GenreId, Genre FROM Genre";
				SqlCommand cmd = new SqlCommand(cmdText, conn);
				conn.Open();
				SqlDataReader reader = cmd.ExecuteReader();
				if (reader.HasRows)
				{
					while (reader.Read())
					{
						GenreInfo genre = new GenreInfo();
						genre.GenreID = reader.GetInt32(0);
						genre.Genre = reader.GetString(1);
						genre.IsSelected = true;
						Genres.Add(genre);
					}

				}
			}
		}
		//populates the list of ratings, similar to how it is done in Add Movie script
		private void PopulateRatingList()
		{
			using (SqlConnection conn = new SqlConnection(SecurityHelper.GetDBConnectionString()))
			{
				string cmdText = "SELECT RatingID, RatingName FROM Rating";
				SqlCommand cmd = new SqlCommand(cmdText, conn);
				conn.Open();
				SqlDataReader reader = cmd.ExecuteReader();
				if (reader.HasRows)
				{
					while (reader.Read())
					{
						RatingInfo rating = new RatingInfo();
						rating.RatingID = reader.GetInt32(0);
						rating.RatingName = reader.GetString(1);
						rating.IsSelected = true;
						Ratings.Add(rating);
					}

				}
			}
		}


		//populates all movies from database using select statement
		private void PopulateMovie()
		{
			Movies.Clear();
			using (SqlConnection conn = new SqlConnection(SecurityHelper.GetDBConnectionString()))
			{
				string cmdText = "SELECT MovieName, MovieDesc, MinRuntime, RatingID, GenreID, MovieID, Image, ReleaseDate FROM Movie";
				SqlCommand cmd = new SqlCommand(cmdText, conn);
				conn.Open();
				SqlDataReader reader = cmd.ExecuteReader();
				if (reader.HasRows)
				{
					while (reader.Read())
					{
						//storing those movies into the list of movies
						var movie = new Movie();
						movie.MovieName = reader.GetString(0);
						movie.MovieDesc = reader.GetString(1);
						movie.MinRuntime = reader.GetInt32(2);
						movie.RatingID = reader.GetInt32(3);
						movie.GenreID = reader.GetInt32(4);
						movie.MovieID = reader.GetInt32(5);
						movie.Image = reader.GetString(6);
						movie.ReleaseDate = reader.GetDateTime(7);
						Movies.Add(movie);
					}
				}
			}
		}
	}

	public class GenreInfo
	{	
		public int GenreID { get; set; }
		public string Genre {  get; set; }
		public bool IsSelected { get; set; }

	}

	public class RatingInfo
	{
		public int RatingID { get; set;}
		public string RatingName { get; set;}
		public bool IsSelected { get; set; }
	}

}
