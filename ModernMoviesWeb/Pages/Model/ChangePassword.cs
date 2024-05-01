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
		public string NewPassword { get; set; }

		public int UserID { get; set; }
	}
}
