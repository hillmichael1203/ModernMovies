﻿using System.ComponentModel.DataAnnotations;

namespace ModernMoviesWeb.Pages.Model
{
	public class ChangePassword
	{
		// Required attribute ensures the Old Password field is not left empty.
		[Required(ErrorMessage = "The Old Password field is required.")]
		[Display(Name = "Old Password")]
		public string OldPassword { get; set; }

		// Required attribute ensures the New Password field is not left empty.
		[Required(ErrorMessage = "The New Password field is required.")]
		[Display(Name = "New Password")]
		[RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^a-zA-Z\d]).{10,16}$", ErrorMessage = "Password must be 10-16 characters long and include at least one uppercase letter, one lowercase letter, one number, and one special character.")]

		public string NewPassword { get; set; }

		// Property to store the User ID.
		public int UserID { get; set; }
	}
}
