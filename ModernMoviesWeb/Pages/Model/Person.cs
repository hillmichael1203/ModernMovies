using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace ModernMoviesWeb.Pages.Model
{
	public class Person
	{

		// Unique identifier for a user, automatically managed by the database.
		public int PersonId { get;set; }

		// The name of the user, required field.
		[Required(ErrorMessage = "The Name field is required.")]
		[Display(Name = "Name")]
		public string Name { get;set; }

		// The email address of the user, required for login.
		[Required(ErrorMessage = "The Email Address field is required.")]
		[Display(Name = "Email Address")]
		public string Email { get;set; }

		// The password for the user's account, required.
		[Required(ErrorMessage = "The Password field is required.")]
		[Display(Name = "Password")]

		[RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^a-zA-Z\d]).{10,16}$", ErrorMessage = "Password must be 10-16 characters long and include at least one uppercase letter, one lowercase letter, one number, and one special character.")]
		public string Password { get;set; }

		// Optional phone number for the user, not a required field.
		[Display(Name = "Phone Number")]
		public string? PhoneNumber { get;set; }

		// Role identifier linking to the role the user has (admin, customer, etc.). Not displayed in UI.
		[Display(Name = "Role")]
		public int RoleId { get;set; }

		// Tracks the last time a person logged in.
		[Display(Name = "Last Login")]
		public DateTime LastLoginTime { get;set; }
	}
}
