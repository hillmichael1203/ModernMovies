using System.ComponentModel.DataAnnotations;

namespace ModernMoviesWeb.Pages.Model
{
	public class Profile
	{

		// Unique identifier for a user, same as in the Person model.
		public int PersonId { get; set; }

		// Displays the name of the user. Used in user profile pages.
		[Display(Name = "Name")]
		public string Name { get; set; }

		// Displays the email address of the user.
		[Display(Name = "Email Address")]
		public string Email { get; set; }

		// Displays the password, where users can update their password.
		[Display(Name = "Password")]
		public string Password { get; set; }

		// Displays user's phone number if provided.
		[Display(Name = "Phone Number")]
		public string? PhoneNumber { get; set; }

		// Displays the last login time of the user.
		public DateTime LastLoginTime { get; set; }
	}
}
