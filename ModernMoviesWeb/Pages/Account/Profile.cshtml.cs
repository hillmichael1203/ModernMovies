using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using ModernMoviesBusiness;
using ModernMoviesWeb.Pages.Model;
using System.Security.Claims;

namespace ModernMoviesWeb.Pages.Account
{
	//Only someone that is logged in can access this page.
	[Authorize(Roles = "Customer,Employee,Administrator")]
	[BindProperties]
	public class ProfileModel : PageModel
    {
		public UserProfile profile { get; set; } = new UserProfile();
        public void OnGet()
        {
			PopulateProfile();
        }

		public void PopulateProfile()
		{
			//Retreives the email from the associated cookie to find the correalated database entry.
			string email = HttpContext.User.FindFirstValue(ClaimTypes.Email);
			using (SqlConnection conn = new SqlConnection(SecurityHelper.GetDBConnectionString()))
			{
				//Create a SQL command
				string cmdText = "SELECT Name, Email, PhoneNumber, LastLoginTime, UserID FROM Person WHERE Email=@email";
				SqlCommand cmd = new SqlCommand(cmdText, conn);
				cmd.Parameters.AddWithValue("@email", email);
				//Open the database
				conn.Open();
				//Execute the SQL command
				SqlDataReader reader = cmd.ExecuteReader();
				if (reader.HasRows)
				{
					//Send the Users information to Profile.cshtml be output.
					reader.Read();
					profile.Name = reader.GetString(0);
					profile.Email = reader.GetString(1);
					profile.PhoneNumber = reader.GetString(2);
					profile.LastLoginTime = reader.GetDateTime(3);
					profile.PersonId = reader.GetInt32(4);
				}
			}
		}
    }
}
