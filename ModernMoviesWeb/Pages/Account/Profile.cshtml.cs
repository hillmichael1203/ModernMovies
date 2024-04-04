using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ModernMoviesWeb.Pages.Model;

namespace ModernMoviesWeb.Pages.Account
{
    public class ProfileModel : PageModel
    {
		[BindProperty]
		public Profile profile { get; set; }
        public void OnGet()
        {
			profile = new Profile();
			profile.Name = "Jeffrey Epstein";
			profile.PhoneNumber = "5318008";
			profile.Email = "test@test.test";
			profile.LastLoginTime = DateTime.Now;
        }
    }
}
