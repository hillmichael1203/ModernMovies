using System.ComponentModel.DataAnnotations;

namespace ModernMoviesWeb.Pages.Model
{
	public class Login
	{

		// Requires the user to provide an email address. If not provided, shows an error message.
		[Required(ErrorMessage = "The Email Address field is required.")]
		[Display(Name = "Email Address")]
		public string Email { get; set; }

		// Requires the user to provide a password. If not provided, shows an error message.
		[Required(ErrorMessage = "The Password field is required.")]
		[Display(Name = "Password")]
		public string Password { get; set; }
		
	}
}
