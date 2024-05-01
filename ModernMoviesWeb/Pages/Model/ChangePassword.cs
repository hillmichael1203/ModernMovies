using System.ComponentModel.DataAnnotations;

namespace ModernMoviesWeb.Pages.Model
{
	public class ChangePassword
	{
		[Required(ErrorMessage = "The Old Password field is required.")]
		[Display(Name = "Old Password")]
		public string OldPassword { get; set; }
		[Required(ErrorMessage = "The New Password field is required.")]
		[Display(Name = "New Password")]
		[RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^a-zA-Z\d]).{10,16}$", ErrorMessage = "Password must be 10-16 characters long and include at least one uppercase letter, one lowercase letter, one number, and one special character.")]

		public string NewPassword { get; set; }

		public int UserID { get; set; }
	}
}
