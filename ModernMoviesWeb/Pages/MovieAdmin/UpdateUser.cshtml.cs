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
		[BindProperty]
		public Person updatedUser { get; set; } = new Person();
		public void OnGet(int id)
		{
			using (SqlConnection conn = new SqlConnection(SecurityHelper.GetDBConnectionString()))
			{
				//pulls relevant user info to load into list of users
				string cmdText = "SELECT UserID, Name, Email, Password, PhoneNumber, TypeID, LastLoginTime FROM Person WHERE UserID=@userID";
				SqlCommand cmd = new SqlCommand(cmdText, conn);
				cmd.Parameters.AddWithValue("@userID", id);
				conn.Open();
				SqlDataReader reader = cmd.ExecuteReader();
				if (reader.HasRows)
				{
					//placing the user info into the user object to be used in HTML
					while (reader.Read())
					{
						updatedUser.PersonId = reader.GetInt32(0);
						updatedUser.Name = reader.GetString(1);
						updatedUser.Email = reader.GetString(2);
						updatedUser.Password = reader.GetString(3);
						updatedUser.PhoneNumber = reader.GetString(4);
						updatedUser.RoleId = reader.GetInt32(5);
						updatedUser.LastLoginTime = reader.GetDateTime(6);
					}
				}
			}
		}
	}
}
