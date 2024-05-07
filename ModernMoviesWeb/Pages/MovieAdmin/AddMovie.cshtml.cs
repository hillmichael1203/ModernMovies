using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using ModernMoviesBusiness;
using ModernMoviesWeb.Pages.Model;
using System.Data.SqlTypes;

namespace ModernMoviesWeb.Pages.MovieAdmin
{
	//admin only page
	[Authorize(Roles = "Administrator")]
	[BindProperties]
    public class AddMovieModel : PageModel
    {
		public Movie newMovie { get; set; } = new Movie();

		//values for ratings and genres
		public List<SelectListItem> Genres { get; set; } = new List<SelectListItem>();
		public List<SelectListItem> Ratings { get; set; } = new List<SelectListItem>();
        public void OnGet()
        {
			PopulateGenreDDL();
			PopulateRatingDDL();
        }

		//when user presses add movie
		public IActionResult OnPost()
		{
			if (ModelState.IsValid)
			{
				using (SqlConnection conn = new SqlConnection(SecurityHelper.GetDBConnectionString()))
				{
					//adding the new movie with the values from the HTML
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
				//populate dropdowns and refresh page
				PopulateGenreDDL();
				PopulateRatingDDL();
				return Page();
			}
		}

		//method to pull the values of the rating IDs from the database
		private void PopulateRatingDDL()
		{
			using (SqlConnection conn = new SqlConnection(SecurityHelper.GetDBConnectionString()))
			{
				//pulling the names from the ratings
				string cmdText = "SELECT RatingID, RatingName FROM Rating";
				SqlCommand cmd = new SqlCommand(cmdText, conn);
				conn.Open();
				SqlDataReader reader = cmd.ExecuteReader();
				if (reader.HasRows)
				{
					//adding the ratings to the list Ratings declared previously
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

		//method to pull the values of the genre IDs from the database
		private void PopulateGenreDDL()
		{
			using(SqlConnection conn = new SqlConnection(SecurityHelper.GetDBConnectionString()))
			{
				//pulling the names from the genres
				string cmdText = "SELECT GenreId, Genre FROM Genre";
				SqlCommand cmd = new SqlCommand(cmdText, conn);
				conn.Open();
				SqlDataReader reader = cmd.ExecuteReader();
				if(reader.HasRows)
				{                   
					//adding the genres to the list Genres declared previously
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
	}
}
