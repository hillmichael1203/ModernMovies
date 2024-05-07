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
    public class ViewMoviesModel : PageModel
    {
		//list of movie genres from database
		public List<SelectListItem> Genres { get; set; } = new List<SelectListItem>();

		//list of movies from database to be used in foreach loop in HTML
		public List<Movie> Movies { get; set; } = new List<Movie>();

		public int SelectedGenreId { get; set; }
		
        public void OnGet()
        {
			//autopopulates with movies from the action genre
			PopulateMovie(0);
			PopulateGenreDDL();
        }

		public void OnPost()
		{
			//changes which genre is displayed
			PopulateMovie(SelectedGenreId);
			PopulateGenreDDL();
		}

		private void PopulateMovie(int id)
		{
			using(SqlConnection conn = new SqlConnection(SecurityHelper.GetDBConnectionString()))
			{
				//pulls relevant movie info to load into list of movies
				string cmdText = "SELECT MovieName, MovieDesc, MinRuntime, RatingID, GenreID, MovieID, Image, ReleaseDate FROM Movie WHERE GenreID=@genreID";
				SqlCommand cmd = new SqlCommand(cmdText, conn);
				cmd.Parameters.AddWithValue("@genreID", id);
				conn.Open();
				SqlDataReader reader = cmd.ExecuteReader();
				if(reader.HasRows)
				{
					//placing the movie info into the movie object to be used in HTML
					while (reader.Read())
					{
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

		//populates the genre dropdown list from the database, identical to how it works in Add and Edit Movie pages
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
						if(genre.Value == SelectedGenreId.ToString())
						{
							genre.Selected = true;
						}
						Genres.Add(genre);
					}

				}
			}
		}
	}
}
