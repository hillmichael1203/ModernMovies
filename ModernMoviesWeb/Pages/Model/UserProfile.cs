using System.ComponentModel.DataAnnotations;

namespace ModernMoviesWeb.Pages.Model
{
	public class UserProfile
	{
		// Unique identifier for a user, same as in the Person model.

		public int PersonId { get; set; }

		// Displays the name of the user. Used in user profile pages.
		public string Name { get; set; }


		// Displays the email address of the user.
		public string Email { get; set; }

		// Displays the password, where users can update their password.
		public string Password { get; set; }

		// Displays user's phone number if provided.
		public string? PhoneNumber { get; set; }

		// Displays the last login time of the user.
		public DateTime LastLoginTime { get; set; }

	}
}
