using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using ModernMoviesBusiness;

namespace ModernMoviesWeb.Pages.MovieAdmin
{
	//admin only page
	[Authorize(Roles = "Administrator")]

	public class DeleteMovieModel : PageModel
    {
        public IActionResult OnGet(int id)
        {
			using (SqlConnection conn = new SqlConnection(SecurityHelper.GetDBConnectionString()))
			{
				//deleting the movie from the database
				string cmdText = "DELETE FROM Movie WHERE MovieID=@movieID";
				SqlCommand cmd = new SqlCommand(cmdText, conn);
				cmd.Parameters.AddWithValue("movieID", id);
				conn.Open();
				cmd.ExecuteNonQuery();
				//returning to view movies page
				return RedirectToPage("ViewMovies");
			}
        }
    }
}
