using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using ModernMoviesBusiness;
using ModernMoviesWeb.Pages.Model;
using System.Data.SqlTypes;

namespace ModernMoviesWeb.Pages.MovieAdmin
{
    public class AddMovieModel : PageModel
    {
		public Movie newMovie { get; set; } = new Movie();

		public List<SelectListItem> Genres { get; set; } = new List<SelectListItem>();
		public List<SelectListItem> Ratings { get; set; } = new List<SelectListItem>();
        public void OnGet()
        {
			PopulateGenreDDL();
			PopulateRatingDDL();
        }

		public IActionResult OnPost()
		{
			if (ModelState.IsValid)
			{
				using (SqlConnection conn = new SqlConnection(SecurityHelper.GetDBConnectionString()))
				{
					string cmdText = "INSERT INTO Movie(MovieName, MovieDesc, MinRuntime, RatingID, GenreID, Image, ReleaseDate) " +
						"VALUES (@movieName, @movieDesc, @minRuntime, @ratingID, @genreID, @image, @releaseDate)";
					SqlCommand cmd = new SqlCommand(cmdText, conn);
					cmd.Parameters.AddWithValue("@movieName", newMovie.MovieName);
					cmd.Parameters.AddWithValue("@movieDesc", newMovie.MovieDesc);
					cmd.Parameters.AddWithValue("@minRuntime", newMovie.MinRuntime);
					cmd.Parameters.AddWithValue("@ratingID", newMovie.RatingID);
					cmd.Parameters.AddWithValue("@genreID", newMovie.GenreID);
					cmd.Parameters.AddWithValue("@image", newMovie.Image);
					cmd.Parameters.AddWithValue("@releaseDate", newMovie.ReleaseDate);


					conn.Open();
					cmd.ExecuteNonQuery();
					return RedirectToPage("ViewMovies");
				}
			}
			else
			{
				PopulateGenreDDL();
				PopulateRatingDDL();
				return Page();
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

		private void PopulateGenreDDL()
		{
			using(SqlConnection conn = new SqlConnection(SecurityHelper.GetDBConnectionString()))
			{
				string cmdText = "SELECT GenreId, Genre FROM Genre";
				SqlCommand cmd = new SqlCommand(cmdText, conn);
				conn.Open();
				SqlDataReader reader = cmd.ExecuteReader();
				if(reader.HasRows)
				{
					while(reader.Read())
					{
						var genre = new SelectListItem();
						genre.Value = reader.GetInt32(0).ToString();
						genre.Text = reader.GetString(1);
						Genres.Add(genre);
					}
					
				}
			}
		}
	}
}
