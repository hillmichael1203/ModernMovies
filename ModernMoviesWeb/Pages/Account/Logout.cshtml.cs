using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ModernMoviesWeb.Pages.Account
{
    public class LogoutModel : PageModel
    {
		public IActionResult OnPost()
		{
			//Cookie Authentication is removed and User is sent back to home page.
			HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
			return RedirectToPage("/Index");
		}

	public void OnGet()
        {
        }
    }
}
