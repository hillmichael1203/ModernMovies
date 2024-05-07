using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ModernMoviesWeb.Pages
{
	public class IndexModel : PageModel
	{
		// Logger field to store logger instance.
		// ILogger logs information and/or errors during execution
		private readonly ILogger<IndexModel> _logger;

		// Constructor that initializes the logger.
		public IndexModel(ILogger<IndexModel> logger)
		{
			_logger = logger;
		}

		// OnGet method to handle GET requests to the Index page.
		// This method runs when the Index page is accessed.
		public void OnGet()
		{

		}
	}
}
