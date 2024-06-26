using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ModernMoviesWeb.Pages.Model;
using Microsoft.Data.SqlClient;
using ModernMoviesBusiness;
using System.Text.RegularExpressions;

namespace ModernMoviesWeb.Pages.Account
{
    public class RegisterModel : PageModel
    {
        [BindProperty]
		public Person newPerson { get; set; }
		public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            if(ModelState.IsValid)
            {
				//Check if the email has been used for an account already before registering.
				if (EmailDoesNotExist(newPerson.Email))
				{
					RegisterUser();
					return RedirectToPage("Login");
				}
				else
				{
					ModelState.AddModelError("RegisterError", "The email has already been used. Please try a different one.");
					return Page();
				}
			}
            else
            {
                return Page();
            }
        }

		private bool EmailDoesNotExist(string email)
		{
			//Searches the database for the given email. True if the email is not present.
			using (SqlConnection conn = new SqlConnection(SecurityHelper.GetDBConnectionString()))
			{
				//Create a SQL command
				string cmdText = "SELECT * FROM Person WHERE Email = @email";
				SqlCommand cmd = new SqlCommand(cmdText, conn);
				cmd.Parameters.AddWithValue("@email", email);
				//Open the database
				conn.Open();
				//Execute the SQL command
				SqlDataReader reader = cmd.ExecuteReader();
				if(reader.HasRows)
				{
					return false;
				}
				else
				{
					return true;
				}
			}
		}

		private void RegisterUser()
		{
			//Adds the new User into the database.
			using (SqlConnection conn = new SqlConnection(SecurityHelper.GetDBConnectionString()))
			{
				//Create a SQL command
				string cmdText = "INSERT INTO Person(Name, Email, Password, PhoneNumber, TypeId, LastLoginTime) " +
					"VALUES (@name, @email, @password, @phoneNumber, 0, @lastLoginTime )";
				SqlCommand cmd = new SqlCommand(cmdText, conn);
				cmd.Parameters.AddWithValue("@name", newPerson.Name);
				cmd.Parameters.AddWithValue("@email", newPerson.Email);
				cmd.Parameters.AddWithValue("@password", SecurityHelper.GeneratePasswordHash(newPerson.Password));
				//Phone number is an optional entry for the user.
				if (newPerson.PhoneNumber != null)
				{
					cmd.Parameters.AddWithValue("@phoneNumber", newPerson.PhoneNumber);
				}
				else
				{
					cmd.Parameters.AddWithValue("@phoneNumber", "000-000-0000");
				}
				cmd.Parameters.AddWithValue("@lastLoginTime", DateTime.Now.ToString());
				//Open the database
				conn.Open();
				//Execute the SQL command
				cmd.ExecuteNonQuery();
			}
		}
    }
}
