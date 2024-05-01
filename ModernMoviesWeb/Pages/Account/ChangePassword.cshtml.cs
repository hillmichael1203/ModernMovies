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
				if (IsPasswordValid(changePassword.NewPassword))
				{
					using (SqlConnection conn = new SqlConnection(SecurityHelper.GetDBConnectionString()))
					{
						string cmdText = "SELECT Password FROM Person WHERE UserID=@userID";
						SqlCommand cmd = new SqlCommand(cmdText, conn);
						cmd.Parameters.AddWithValue("@userID", id);
						conn.Open();
						SqlDataReader reader = cmd.ExecuteReader();

						if (reader.HasRows)
						{
							reader.Read();
							if (reader.IsDBNull(0))
							{
								ModelState.AddModelError("changePassword.OldPassword", "How'd you even do that? This account doesn't exist.");
								return Page();
							}
							else
							{
								string passwordHash = reader.GetString(0);
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
							ModelState.AddModelError("changePassword.OldPassword", "How'd you even do that? This account doesn't exist.");
							return Page();
						}
					}
				}
				else
				{
					ModelState.AddModelError("newPerson.Password", "Password must be 10-16 characters long and include at least one uppercase letter, one lowercase letter, one number, and one special character.");
					return Page();
				}
				}
				else
				{
					return Page();
				}

			}

		private bool IsPasswordValid(string password)
		{
			var regex = new Regex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^a-zA-Z\d]).{10,16}$");

			return regex.IsMatch(password);
		}

		private void ChangePassword(int id)
		{
			using (SqlConnection conn = new SqlConnection(SecurityHelper.GetDBConnectionString()))
			{
				//2. create a SQL command
				string cmdText = "UPDATE Person SET Password = @password WHERE UserID = @userID";
				SqlCommand cmd = new SqlCommand(cmdText, conn);
				cmd.Parameters.AddWithValue("@password", SecurityHelper.GeneratePasswordHash(changePassword.NewPassword));
				cmd.Parameters.AddWithValue("@userID", id);
				//3. open the database
				conn.Open();
				//4. execute the SQL command
				cmd.ExecuteNonQuery();
				conn.Close();
			}
		}
	}
}
