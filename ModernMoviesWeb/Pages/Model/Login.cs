using System.ComponentModel.DataAnnotations;

namespace ModernMoviesWeb.Pages.Model
{
	public class Login
	{
		[Required(ErrorMessage = "The Email Address field is required.")]
		[Display(Name = "Email Address")]
		public string Email { get; set; }
		[Required(ErrorMessage = "The Password field is required.")]
		[Display(Name = "Password")]
		public string Password { get; set; }
		
	}
}
