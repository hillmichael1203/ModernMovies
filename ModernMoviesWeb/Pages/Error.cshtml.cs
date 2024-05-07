using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Diagnostics;

namespace ModernMoviesWeb.Pages
{
	// Attributes to disable caching for this page and ignore antiforgery token requirements.
	[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    [IgnoreAntiforgeryToken]
    public class ErrorModel : PageModel
    {
		// Property to store the Request ID, which is useful for correlating error logs.
		public string? RequestId { get; set; }

		// Boolean property to determine if the Request ID should be shown in the UI.
		public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);

		// Logger field to store logger instance.
		// ILogger logs information and/or errors during execution
		private readonly ILogger<ErrorModel> _logger;

		// Constructor that initializes the logger.
		public ErrorModel(ILogger<ErrorModel> logger)
        {
            _logger = logger;
        }

        // OnGet method to handle GET requests to the error page.
        // This method runs when the error page is accessed and sets the RequestId for tracking purposes.
		public void OnGet()
        {
            RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
        }
    }

}
