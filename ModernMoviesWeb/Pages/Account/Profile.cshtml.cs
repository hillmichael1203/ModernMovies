using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using ModernMoviesBusiness;
using ModernMoviesWeb.Pages.Model;
using System.Security.Claims;

namespace ModernMoviesWeb.Pages.Account
{
    public class ProfileModel : PageModel
    {
		[BindProperty]
		public UserProfile profile { get; set; } = new UserProfile();
        public void OnGet()
        {
			PopulateProfile();
        }

		public void PopulateProfile()
		{
			string email = HttpContext.User.FindFirstValue(ClaimTypes.Email);
			using (SqlConnection conn = new SqlConnection(SecurityHelper.GetDBConnectionString()))
			{
				string cmdText = "SELECT Name, Email, PhoneNumber, LastLoginTime FROM Person WHERE Email=@email";
				SqlCommand cmd = new SqlCommand(cmdText, conn);
				cmd.Parameters.AddWithValue("@email", email);
				conn.Open();
				SqlDataReader reader = cmd.ExecuteReader();
				if (reader.HasRows)
				{
					reader.Read();
					profile.Name = reader.GetString(0);
					profile.Email = reader.GetString(1);
					profile.PhoneNumber = reader.GetString(2);
					profile.LastLoginTime = reader.GetDateTime(3);
				}
			}
		}
    }
}
