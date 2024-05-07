using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using ModernMoviesBusiness;
using ModernMoviesWeb.Pages.Model;
using System.Security.Claims;

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
			//Redirects to Profile page once valid credentials are used, otherwise stays at Login.
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
			//Gets database entry associated with entered email.
			using (SqlConnection conn = new SqlConnection(SecurityHelper.GetDBConnectionString()))
			{
				//Create a SQL command
				string cmdText = "SELECT Password, UserID, Name, TypeName FROM Person " +
					"INNER JOIN [AccountType] ON Person.TypeId = [AccountType].TypeId WHERE Email=@email";
				SqlCommand cmd = new SqlCommand(cmdText, conn);
				cmd.Parameters.AddWithValue("@email", loginUser.Email);
				//Open the database
				conn.Open();
				//Execute the SQL command
				SqlDataReader reader = cmd.ExecuteReader();

				if(reader.HasRows)
				{
					//False if database entry has no password or does not exist.
					reader.Read();
					if (reader.IsDBNull(0))
					{
						return false;
					}
					else
					{
						string passwordHash = reader.GetString(0);
						//If password matches the database entry create the cookie and log them in. Returns true.
						if(SecurityHelper.VerifyPassword(loginUser.Password, passwordHash))
						{
							int userID = reader.GetInt32(1);
							UpdateLastLoginTime(userID);

							string name = reader.GetString(2);
							string typeName = reader.GetString(3);

							//Create a list of claims
							Claim emailClaim = new Claim(ClaimTypes.Email, loginUser.Email);
							Claim nameClaim = new Claim(ClaimTypes.Name, name);
							Claim roleClaim = new Claim(ClaimTypes.Role, typeName);

							List<Claim> claims = new List<Claim> { emailClaim, nameClaim, roleClaim };

							//Create a ClaimsIdentity
							ClaimsIdentity identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

							//Create a ClaimsPrinciple
							ClaimsPrincipal principal = new ClaimsPrincipal(identity);

							//Create a authentication cookie
							HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);


							return true;
						}
						else
						{
							//False if password is incorrect.
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
			//Update database entry for account with new Login Time.
			using (SqlConnection conn = new SqlConnection(SecurityHelper.GetDBConnectionString()))
			{
				//Create a SQL command
				string cmdText = "UPDATE Person SET LastLoginTime = @lastLoginTime WHERE UserID = @userID";
				SqlCommand cmd = new SqlCommand(cmdText, conn);
				cmd.Parameters.AddWithValue("@lastLoginTime", DateTime.Now.ToString());
				cmd.Parameters.AddWithValue("@userID", userID);
				//Open the database
				conn.Open();
				//Execute the SQL command
				cmd.ExecuteNonQuery();
			}
		}
	}
}
