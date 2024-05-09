using System.ComponentModel.DataAnnotations;

namespace ModernMoviesWeb.Pages.Model
{
	public class EditUser
	{
		// Unique identifier for a user, automatically managed by the database.
		public int PersonId { get; set; }

		// The name of the user, required field.
		[Required(ErrorMessage = "The Name field is required.")]
		[Display(Name = "Name")]
		public string Name { get; set; }

		// The email address of the user, required for login.
		[Required(ErrorMessage = "The Email Address field is required.")]
		[Display(Name = "Email Address")]
		public string Email { get; set; }

		// Optional phone number for the user, not a required field.
		[Display(Name = "Phone Number")]
		public string? PhoneNumber { get; set; }

		// Role identifier linking to the role the user has (admin, customer, etc.). Not displayed in UI.
		[Display(Name = "Role")]
		public int RoleId { get; set; }
	}
}
