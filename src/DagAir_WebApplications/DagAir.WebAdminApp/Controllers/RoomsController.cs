using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace DagAir.WebAdminApp.Controllers
{
    public class RoomsController : Controller
    {
        private readonly ILogger<RoomsController> _logger;
        private readonly IConfiguration _configuration;
        private const string ConfigurationSection = "environment";
        
        public RoomsController(ILogger<RoomsController> logger, 
            IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        [HttpGet]
        public async Task<IActionResult> Rooms()
        {
            var environment = _configuration.GetSection($"{ConfigurationSection}").Value;
            _logger.LogInformation($"Current environment: {environment}");

            var roomModel = new RoomModel();
            if (environment == "docker")
            {
                roomModel.Environment = "appsettings.Docker.json";
                return View(roomModel);
            }
            
            roomModel.Environment = "appsettings.json";

            return View(roomModel);
        }

    }
    
    public class RoomModel
    {
        public string Environment { get; set; }
    }
}