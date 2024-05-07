using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using ModernMoviesBusiness;
using ModernMoviesWeb.Pages.Model;

namespace ModernMoviesWeb.Pages.MovieAdmin
{
	//admin only page
	[Authorize(Roles = "Administrator")]
	[BindProperties]
    public class EditMovieModel : PageModel
    {
		public Movie editedMovie { get; set; } = new Movie();
		
		//lists of genres and movie ratings pulled from database
		public List<SelectListItem> Genres { get; set; } = new List<SelectListItem>();
		public List<SelectListItem> Ratings { get; set; } = new List<SelectListItem>();
		public void OnGet(int id)
        {
			PopulateMovie(id);
			PopulateGenreDDL();
			PopulateRatingDDL();
        }

		//updating data for movie using changed values in model
		public IActionResult OnPost(int id)
		{
			if (ModelState.IsValid)
			{
				using (SqlConnection conn = new SqlConnection(SecurityHelper.GetDBConnectionString()))
				{
					string cmdText = "UPDATE Movie SET MovieName=@movieName, MovieDesc=@movieDesc, MinRuntime=@minRuntime, RatingID=@ratingID, " +
						"GenreID=@genreID, Image=@image, ReleaseDate=@releaseDate WHERE MovieID = @movieID";
					SqlCommand cmd = new SqlCommand(cmdText, conn);
					cmd.Parameters.AddWithValue("@movieName", editedMovie.MovieName);
					cmd.Parameters.AddWithValue("@movieDesc", editedMovie.MovieDesc);
					cmd.Parameters.AddWithValue("@minRuntime", editedMovie.MinRuntime);
					cmd.Parameters.AddWithValue("@ratingID", editedMovie.RatingID);
					cmd.Parameters.AddWithValue("@genreID", editedMovie.GenreID);
					cmd.Parameters.AddWithValue("@image", editedMovie.Image);
					cmd.Parameters.AddWithValue("@releaseDate", editedMovie.ReleaseDate);
					cmd.Parameters.AddWithValue("@movieID", id);

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

		//method to pull the generes from the database and put them into a list to be used on the dropdown, identical to method from Add Movie
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

		//method to pull the ratings from the database and put them into a list to be used on the dropdown, identical to method from Add Movie
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

		//pulls the current movie id's info from the database
		private void PopulateMovie(int id)
		{
			using (SqlConnection conn = new SqlConnection(SecurityHelper.GetDBConnectionString()))
			{
				string cmdText = "SELECT MovieID, MovieName, MovieDesc, MinRuntime, RatingID, GenreID, Image, ReleaseDate FROM Movie WHERE MovieID = @movieID";
				SqlCommand cmd = new SqlCommand(cmdText, conn);
				cmd.Parameters.AddWithValue("@movieId", id);
				conn.Open();
				SqlDataReader reader = cmd.ExecuteReader();
				if(reader.HasRows)
				{
					//pouplating the movie into the proper places in the model so it can be changed
					reader.Read();
					editedMovie.MovieID = id;
					editedMovie.MovieName = reader.GetString(1);
					editedMovie.MovieDesc = reader.GetString(2);
					editedMovie.MinRuntime = reader.GetInt32(3);
					editedMovie.RatingID = reader.GetInt32(4);
					editedMovie.GenreID = reader.GetInt32(5);
					editedMovie.Image = reader.GetString(6);
					editedMovie.ReleaseDate = reader.GetDateTime(7);
				}
			}
		}
	}
}
