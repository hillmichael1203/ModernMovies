using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using ModernMoviesBusiness;
using ModernMoviesWeb.Pages.Model;
using System.Runtime.InteropServices;

namespace ModernMoviesWeb.Pages.MovieAdmin
{
	[Authorize(Roles = "Administrator")]
	public class EditUsersModel : PageModel
	{
		[BindProperty]
		public List<Person> Users { get; set; } = new List<Person>();

		public void OnGet()
		{
			PopulateUsers();
		}

		public IActionResult OnPost()
		{
			Console.WriteLine("HIT!");
			return RedirectToPage("EditUsers");
		}

		public void PopulateUsers()
		{
			using (SqlConnection conn = new SqlConnection(SecurityHelper.GetDBConnectionString()))
			{
				//pulls relevant user info to load into list of users
				string cmdText = "SELECT UserID, Name, Email, Password, PhoneNumber, TypeID, LastLoginTime FROM Person";
				SqlCommand cmd = new SqlCommand(cmdText, conn);
				conn.Open();
				SqlDataReader reader = cmd.ExecuteReader();
				if (reader.HasRows)
				{
					//placing the user info into the user object to be used in HTML
					while (reader.Read())
					{
						var user = new Person();
						user.PersonId = reader.GetInt32(0);
						user.Name = reader.GetString(1);
						user.Email = reader.GetString(2);
						user.Password = reader.GetString(3);
						user.PhoneNumber = reader.GetString(4);
						user.RoleId = reader.GetInt32(5);
						user.LastLoginTime = reader.GetDateTime(6);
						Users.Add(user);
					}
				}
			}
		}
	}
}