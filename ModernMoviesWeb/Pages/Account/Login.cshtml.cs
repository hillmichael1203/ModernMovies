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
			if(ModelState.IsValid)
			{
				SqlConnection conn = new SqlConnection(SecurityHelper.GetDBConnectionString());
				string cmdText = "SELECT Password FROM Person WHERE Email=@email";
				SqlCommand cmd = new SqlCommand(cmdText, conn);
				cmd.Parameters.AddWithValue("@email", loginUser.Email);
				conn.Open();
				SqlDataReader reader = cmd.ExecuteReader();
				if(reader.HasRows)
				{
					reader.Read();
					if(reader.IsDBNull(0))
					{
						string passwordHash = reader.GetString(0);
						if(SecurityHelper.VerifyPassword(loginUser.Password, passwordHash))
						{
							return RedirectToPage("Index");
						}
						else
						{
							ModelState.AddModelError("LoginError", "Invalid credentials. Try again.");
							return Page();
						}
					}
					else
					{
						ModelState.AddModelError("LoginError", "Invalid credentials. Try again.");
						return Page();
					}
				}
				else
				{
					ModelState.AddModelError("LoginError", "Invalid credentials. Try again.");
					return Page();
				}
			}
			else
			{
				ModelState.AddModelError("LoginError", "Invalid credentials. Try again.");
				return Page();
			}
		}
    }
}
