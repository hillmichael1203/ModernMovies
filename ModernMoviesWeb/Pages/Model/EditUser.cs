using System.ComponentModel.DataAnnotations;

namespace ModernMoviesWeb.Pages.Model
{
	public class EditUser
	{
		// Requires the user to provide an email address. If not provided, shows an error message.
		[Required(ErrorMessage = "The Email Address field is required.")]
		[Display(Name = "Email Address")]
		public string Email { get; set; }
	}
}
