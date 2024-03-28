using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace ModernMoviesWeb.Pages.Model
{
	public class Person
	{
		public int PersonId { get;set; }
		[Required(ErrorMessage = "The Name field is required.")]
		[Display(Name = "Name")]
		public string Name { get;set; }
		[Required(ErrorMessage = "The Email Address field is required.")]
		[Display(Name = "Email Address")]
		public string Email { get;set; }
		[Required(ErrorMessage = "The Password field is required.")]
		[Display(Name = "Password")]
		public string Password { get;set; }
		[Display(Name = "Phone Number")]
		public string? PhoneNumber { get;set; }
		public string? RoleId { get;set; }
		public DateTime LastLoginTime { get;set; }
	}
}
