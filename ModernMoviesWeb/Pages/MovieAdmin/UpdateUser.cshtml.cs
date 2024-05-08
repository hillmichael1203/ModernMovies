using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using ModernMoviesBusiness;
using ModernMoviesWeb.Pages.Model;

namespace ModernMoviesWeb.Pages.MovieAdmin
{
	[Authorize(Roles = "Administrator")]

	public class UpdateUserModel : PageModel
    {
		public IActionResult OnGet(Person user)
		{
			using (SqlConnection conn = new SqlConnection(SecurityHelper.GetDBConnectionString()))
			{
				string cmdText = "UPDATE Person SET Name=@name, Email=@email, PhoneNumber=@phoneNumber, TypeID=@typeID WHERE UserID = @userID";
				SqlCommand cmd = new SqlCommand(cmdText, conn);
				cmd.Parameters.AddWithValue("@name", user.Name);
				cmd.Parameters.AddWithValue("@email", user.Email);
				cmd.Parameters.AddWithValue("@phoneNumber", user.PhoneNumber);
				cmd.Parameters.AddWithValue("@typeID", user.RoleId);
				cmd.Parameters.AddWithValue("@userID", user.PersonId);


				conn.Open();
				cmd.ExecuteNonQuery();
				return RedirectToPage("EditUsers");
			}
		}
	}
}
