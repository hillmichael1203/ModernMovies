using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ModernMoviesWeb.Pages.Model;
using Microsoft.Data.SqlClient;

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
                //1. create a database connection string
                string connString = "Server=(localdb)\\MSSQLLocalDB;Database=ModernMovies;Trusted_Connection=true;";
                SqlConnection conn = new SqlConnection(connString);
                //2. create a SQL command
                string cmdText = "INSERT INTO Person(FirstName, LastName, Email, Password, PhoneNumber, RoleId, LastLoginTime) " +
                    "VALUES (@firstName, @lastName, @email, @password, @phoneNumber, 2, @lastLoginTime )";
                SqlCommand cmd = new SqlCommand(cmdText, conn);
                cmd.Parameters.AddWithValue("@firstName", newPerson.FirstName);
                cmd.Parameters.AddWithValue("@lastName", newPerson.LastName);
                cmd.Parameters.AddWithValue("@email", newPerson.Email);
                cmd.Parameters.AddWithValue("@password", newPerson.Password);
                if(newPerson.PhoneNumber != null)
                {
					cmd.Parameters.AddWithValue("@phoneNumber", newPerson.PhoneNumber);
				}
                else
                {
					cmd.Parameters.AddWithValue("@phoneNumber", "000-000-0000");
				}
				cmd.Parameters.AddWithValue("@lastLoginTime", DateTime.Now.ToString());
                //3. open the database
                conn.Open();
                //4. execute the SQL command
                cmd.ExecuteNonQuery();
                //5. close the database
                conn.Close();
                return RedirectToPage("Login");
			}
            else
            {
                return Page();
            }
        }
    }
}
