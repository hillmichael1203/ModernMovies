using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using ModernMoviesBusiness;
using ModernMoviesWeb.Pages.Model;

namespace ModernMoviesWeb.Pages.MovieAdmin
{
	[BindProperties]
    public class EditMovieModel : PageModel
    {
		public Movie editedMovie { get; set; } = new Movie();
		public List<SelectListItem> Genres { get; set; } = new List<SelectListItem>();
		public List<SelectListItem> Ratings { get; set; } = new List<SelectListItem>();
		public void OnGet(int id)
        {
			PopulateMovie(id);
			PopulateGenreDDL();
			PopulateRatingDDL();
        }

		public IActionResult OnPost(int id)
		{
			if (ModelState.IsValid)
			{
				using (SqlConnection conn = new SqlConnection(SecurityHelper.GetDBConnectionString()))
				{
					string cmdText = "UPDATE Movie SET MovieName=@movieName, MovieDesc=@movieDesc, MinRuntime=@minRuntime, RatingID=@ratingID, " +
						"GenreID=@genreID WHERE MovieID = @movieID";
					SqlCommand cmd = new SqlCommand(cmdText, conn);
					cmd.Parameters.AddWithValue("@movieName", editedMovie.MovieName);
					cmd.Parameters.AddWithValue("@movieDesc", editedMovie.MovieDesc);
					cmd.Parameters.AddWithValue("@minRuntime", editedMovie.MinRuntime);
					cmd.Parameters.AddWithValue("@ratingID", editedMovie.RatingID);
					cmd.Parameters.AddWithValue("@genreID", editedMovie.GenreID);
					cmd.Parameters.AddWithValue("@movieID", id);

					conn.Open();
					cmd.ExecuteNonQuery();
					return RedirectToPage("ViewMovies");
				}
			}
			else
			{
				return Page();
			}
		}

		private void PopulateGenreDDL()
		{
			using (SqlConnection conn = new SqlConnection(SecurityHelper.GetDBConnectionString()))
			{
				string cmdText = "SELECT GenreId, Genre FROM Genre ORDER BY Genre";
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
						Genres.Add(genre);
					}

				}
			}
		}

		private void PopulateRatingDDL()
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
						Ratings.Add(rating);
					}

				}
			}
		}

		private void PopulateMovie(int id)
		{
			using (SqlConnection conn = new SqlConnection(SecurityHelper.GetDBConnectionString()))
			{
				string cmdText = "SELECT MovieID, MovieName, MovieDesc, MinRuntime, RatingID, GenreID FROM Movie WHERE MovieID = @movieID";
				SqlCommand cmd = new SqlCommand(cmdText, conn);
				cmd.Parameters.AddWithValue("@movieId", id);
				conn.Open();
				SqlDataReader reader = cmd.ExecuteReader();
				if(reader.HasRows)
				{
					reader.Read();
					editedMovie.MovieID = id;
					editedMovie.MovieName = reader.GetString(1);
					editedMovie.MovieDesc = reader.GetString(2);
					editedMovie.MinRuntime = reader.GetInt32(3);
					editedMovie.RatingID = reader.GetInt32(4);
					editedMovie.GenreID = reader.GetInt32(5);
				}
			}
		}
	}
}
