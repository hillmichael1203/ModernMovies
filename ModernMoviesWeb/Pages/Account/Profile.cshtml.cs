using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ModernMoviesWeb.Pages.Model;

namespace ModernMoviesWeb.Pages.Account
{
    public class ProfileModel : PageModel
    {
		[BindProperty]
		public UserProfile profile { get; set; } = new UserProfile();
        public void OnGet()
        {
			PopulateProfile();
        }

		public void PopulateProfile()
		{
			profile.Name = "Cooper Nusbaum";
			profile.Email = "coopernusbaum@yahoo.com";
			profile.PhoneNumber = "7137055068";
			profile.LastLoginTime = DateTime.Now;
		}
    }
}
