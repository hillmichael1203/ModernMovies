using System.ComponentModel.DataAnnotations;

namespace ModernMoviesWeb.Pages.Model
{
	public class UserProfile
	{
		public int PersonId { get; set; }

		public string Name { get; set; }

		public string Email { get; set; }

		public string Password { get; set; }
		public string? PhoneNumber { get; set; }
		public DateTime LastLoginTime { get; set; }

	}
}
