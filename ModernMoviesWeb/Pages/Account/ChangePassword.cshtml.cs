	using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration.UserSecrets;
using ModernMoviesBusiness;
using ModernMoviesWeb.Pages.Model;
using System.Security.Claims;
using System.Text.RegularExpressions;

namespace ModernMoviesWeb.Pages.Account
{
	//Only someone logged in may access this page.
	[Authorize(Roles = "Customer,Employee,Administrator")]
	[BindProperties]
	public class ChangePasswordModel : PageModel
	{

		public ChangePassword changePassword { get; set; } = new ChangePassword();
		public void OnGet(int id)
		{
			changePassword.UserID = id;
		}


		public IActionResult OnPost(int id)
		{
			if (ModelState.IsValid)
			{
				using (SqlConnection conn = new SqlConnection(SecurityHelper.GetDBConnectionString()))
				{
					//Create a SQL command
					string cmdText = "SELECT Password FROM Person WHERE UserID=@userID";
					SqlCommand cmd = new SqlCommand(cmdText, conn);
					cmd.Parameters.AddWithValue("@userID", id);
					//Open the database
					conn.Open();
					//Execute the SQL command
					SqlDataReader reader = cmd.ExecuteReader();

					if (reader.HasRows)
					{
						reader.Read();
						if (reader.IsDBNull(0))
						{
							//Error if account had no password.
							ModelState.AddModelError("changePassword.OldPassword", "How'd you even do that? This account doesn't exist.");
							return Page();
						}
						else
						{
							string passwordHash = reader.GetString(0);
							//Updates old password to new password if old password was correct. Returns to Profile.
							if (SecurityHelper.VerifyPassword(changePassword.OldPassword, passwordHash))
							{
								ChangePassword(id);
								return RedirectToPage("Profile");
							}
							else
							{
								ModelState.AddModelError("changePassword.OldPassword", "Old Password is incorrect");
								return Page();

							}
						}
					}
					else
					{
						//Error if account does not exist.
						ModelState.AddModelError("changePassword.OldPassword", "How'd you even do that? This account doesn't exist.");
						return Page();
					}
				}
			}
			else
			{
				return Page();
			}

		}

		private void ChangePassword(int id)
		{
			//Updates the database entry for the account with new password.
			using (SqlConnection conn = new SqlConnection(SecurityHelper.GetDBConnectionString()))
			{
				//Create a SQL command
				string cmdText = "UPDATE Person SET Password = @password WHERE UserID = @userID";
				SqlCommand cmd = new SqlCommand(cmdText, conn);
				cmd.Parameters.AddWithValue("@password", SecurityHelper.GeneratePasswordHash(changePassword.NewPassword));
				cmd.Parameters.AddWithValue("@userID", id);
				//Open the database
				conn.Open();
				//Execute the SQL command
				cmd.ExecuteNonQuery();
				conn.Close();
			}
		}
	}
}
