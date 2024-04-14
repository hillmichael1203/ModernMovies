using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using ModernMoviesBusiness;

namespace ModernMoviesWeb.Pages.MoviePages
{
	[BindProperties]
	public class MoviesModel : PageModel
	{
		public List<String> Genres { get; set; } = new List<String>();
		public List<String> Ratings { get; set; } = new List<String>();
		public void OnGet()
		{
			PopulateGenreList();
			PopulateRatingList();
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
	}
}
