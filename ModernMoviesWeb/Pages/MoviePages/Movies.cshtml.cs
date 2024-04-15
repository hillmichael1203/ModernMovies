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
		public List<String> Genres { get; set; } = new List<String>();
		public List<String> Ratings { get; set; } = new List<String>();
		public List<Movie> Movies { get; set; } = new List<Movie>();
		public void OnGet()
		{
			PopulateGenreList();
			PopulateRatingList();
			PopulateMovie();
		}

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
						var genre = new SelectListItem();
						genre.Value = reader.GetInt32(0).ToString();
						genre.Text = reader.GetString(1);
						Genres.Add(genre.Text);
					}

				}
			}
		}

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
						var rating = new SelectListItem();
						rating.Value = reader.GetInt32(0).ToString();
						rating.Text = reader.GetString(1);
						Ratings.Add(rating.Text);
					}

				}
			}
		}

		private void PopulateMovie()
		{
			using (SqlConnection conn = new SqlConnection(SecurityHelper.GetDBConnectionString()))
			{
				string cmdText = "SELECT MovieName, MovieDesc, MinRuntime, RatingID, GenreID, MovieID FROM Movie";
				SqlCommand cmd = new SqlCommand(cmdText, conn);
				conn.Open();
				SqlDataReader reader = cmd.ExecuteReader();
				if (reader.HasRows)
				{
					while (reader.Read())
					{
						var movie = new Movie();
						movie.MovieName = reader.GetString(0);
						movie.MovieDesc = reader.GetString(1);
						movie.MinRuntime = reader.GetInt32(2);
						movie.RatingID = reader.GetInt32(3);
						movie.GenreID = reader.GetInt32(4);
						movie.MovieID = reader.GetInt32(5);
						Movies.Add(movie);
					}
				}
			}
		}
	}
}
