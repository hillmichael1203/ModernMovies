using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using ModernMoviesBusiness;
using ModernMoviesWeb.Pages.Model;

namespace ModernMoviesWeb.Pages.Account
{
    public class LoginModel : PageModel
    {
		[BindProperty]
		public Login loginUser { get; set; }
        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
			if (ModelState.IsValid)
			{
				if(ValidateCredentials())
				{
					return RedirectToPage("Profile");
				}
				else
				{
					ModelState.AddModelError("LoginError", "Invalid credentials. Try again.");
					return Page();
				}
			}
			else
			{
				
				return Page();
			}
			
		}

		private bool ValidateCredentials()
		{
			using (SqlConnection conn = new SqlConnection(SecurityHelper.GetDBConnectionString()))
			{
				string cmdText = "SELECT Password, UserID FROM Person WHERE Email=@email";
				SqlCommand cmd = new SqlCommand(cmdText, conn);
				cmd.Parameters.AddWithValue("@email", loginUser.Email);
				conn.Open();
				SqlDataReader reader = cmd.ExecuteReader();

				if(reader.HasRows)
				{
					reader.Read();
					if (reader.IsDBNull(0))
					{
						return false;
					}
					else
					{
						string passwordHash = reader.GetString(0);
						if(SecurityHelper.VerifyPassword(loginUser.Password, passwordHash))
						{
							int userID = reader.GetInt32(1);
							UpdateLastLoginTime(userID);
							return true;
						}
						else
						{
							return false;
						}
					}
				}
				else
				{
					return false;
				}
			}
		}

		private void UpdateLastLoginTime(int userID)
		{
			using (SqlConnection conn = new SqlConnection(SecurityHelper.GetDBConnectionString()))
			{
				string cmdText = "UPDATE Person SET LastLoginTime = @lastLoginTime WHERE UserID = @userID";
				SqlCommand cmd = new SqlCommand(cmdText, conn);
				cmd.Parameters.AddWithValue("@lastLoginTime", DateTime.Now.ToString());
				cmd.Parameters.AddWithValue("@userID", userID);
				conn.Open();
				cmd.ExecuteNonQuery();
			}
		}
	}
}
