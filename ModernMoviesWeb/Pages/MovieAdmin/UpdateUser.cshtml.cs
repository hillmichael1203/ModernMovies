using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using ModernMoviesBusiness;
using ModernMoviesWeb.Pages.Model;

namespace ModernMoviesWeb.Pages.MovieAdmin
{
	//Only an administrator can access this page.
	[Authorize(Roles = "Administrator")]

	public class UpdateUserModel : PageModel
	{
		[BindProperty]
		public EditUser updatedUser { get; set; } = new EditUser();
		public List<SelectListItem> Roles { get; set; } = new List<SelectListItem>();
		public void OnGet(int id)
		{
			//Load in relevant info.
			PopulateUser(id);
			PopulateRolesDDL();
		}

		public IActionResult OnPost(int id)
		{
			if (ModelState.IsValid)
			{
				using (SqlConnection conn = new SqlConnection(SecurityHelper.GetDBConnectionString()))
				{
					//Updates the database to conatain new user info.
					string cmdText = "UPDATE Person SET Name=@name, Email=@email, PhoneNumber=@phoneNumber, TypeID=@typeID WHERE UserID = @userID";
					SqlCommand cmd = new SqlCommand(cmdText, conn);
					cmd.Parameters.AddWithValue("@name", updatedUser.Name);
					cmd.Parameters.AddWithValue("@email", updatedUser.Email);
					cmd.Parameters.AddWithValue("@phoneNumber", updatedUser.PhoneNumber);
					cmd.Parameters.AddWithValue("@typeID", updatedUser.RoleId);
					cmd.Parameters.AddWithValue("@userID", id);

					conn.Open();
					cmd.ExecuteNonQuery();
					return RedirectToPage("EditUsers");
				}
			}
			else
			{
				//Returns user input errors
				PopulateUser(id);
				return Page();
			}
		}

		public void PopulateUser(int id)
		{
			using (SqlConnection conn = new SqlConnection(SecurityHelper.GetDBConnectionString()))
			{
				//Loads relevant user info.
				string cmdText = "SELECT UserID, Name, Email, PhoneNumber, TypeID FROM Person WHERE UserID=@userID";
				SqlCommand cmd = new SqlCommand(cmdText, conn);
				cmd.Parameters.AddWithValue("@userID", id);
				conn.Open();
				SqlDataReader reader = cmd.ExecuteReader();
				if (reader.HasRows)
				{
					//placing the user info into the updatedUser object to be used in HTML.
					while (reader.Read())
					{
						updatedUser.PersonId = reader.GetInt32(0);
						updatedUser.Name = reader.GetString(1);
						updatedUser.Email = reader.GetString(2);
						updatedUser.PhoneNumber = reader.GetString(3);
						updatedUser.RoleId = reader.GetInt32(4);
					}
				}
			}
		}

		public void PopulateRolesDDL()
		{
			//Fills out drop down list for roles.
			using (SqlConnection conn = new SqlConnection(SecurityHelper.GetDBConnectionString()))
			{
				string cmdText = "SELECT TypeID, TypeName FROM AccountType ORDER BY TypeID";
				SqlCommand cmd = new SqlCommand(cmdText, conn);
				conn.Open();
				SqlDataReader reader = cmd.ExecuteReader();
				if (reader.HasRows)
				{
					while (reader.Read())
					{
						var type = new SelectListItem();
						type.Value = reader.GetInt32(0).ToString();
						type.Text = reader.GetString(1);
						Roles.Add(type);
					}

				}
			}
		}
	}
}
