using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using ModernMoviesBusiness;

namespace ModernMoviesWeb.Pages.MovieAdmin
{
	[Authorize(Roles = "Administrator")]
	public class DeleteUserModel : PageModel
    {
		public IActionResult OnGet(int id)
		{
			using (SqlConnection conn = new SqlConnection(SecurityHelper.GetDBConnectionString()))
			{
				//deleting the movie from the database
				string cmdText = "DELETE FROM Person WHERE UserID=@userID";
				SqlCommand cmd = new SqlCommand(cmdText, conn);
				cmd.Parameters.AddWithValue("userID", id);
				conn.Open();
				cmd.ExecuteNonQuery();
				//returning to view movies page
				return RedirectToPage("EditUsers");
			}
		}
	}
}
