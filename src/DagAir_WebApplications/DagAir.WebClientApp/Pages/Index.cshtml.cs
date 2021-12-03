using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace DagAir.WebClientApp.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IConfiguration _configuration;
        private const string ConfigurationSection = "environment";
        public string Environment { get; set; }

        public IndexModel(ILogger<IndexModel> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        public void OnGet()
        {
            var environment = _configuration.GetSection($"{ConfigurationSection}").Value;
            _logger.LogInformation($"Current environment: {environment}");
            if (environment == "docker")
            {
                Environment = "appsettings.Docker.json";
                return;
            }
            
            Environment = "appsettings.json";
        }
    }
}
