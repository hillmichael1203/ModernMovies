using System.ComponentModel.DataAnnotations;

namespace ModernMoviesWeb.Pages.Model
{
	public class Profile
	{

		public int PersonId { get; set; }
		[Display(Name = "Name")]
		public string Name { get; set; }
		[Display(Name = "Email Address")]
		public string Email { get; set; }
		[Display(Name = "Password")]
		public string Password { get; set; }
		[Display(Name = "Phone Number")]
		public string? PhoneNumber { get; set; }
		public DateTime LastLoginTime { get; set; }
	}
}
