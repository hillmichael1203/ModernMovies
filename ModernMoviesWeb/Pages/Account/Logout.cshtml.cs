using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ModernMoviesWeb.Pages.Account
{
    public class LogoutModel : PageModel
    {
		public IActionResult OnPost()
		{
			HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
			return RedirectToPage("/Index");
		}

	public void OnGet()
        {
        }
    }
}
