using System;
using System.Threading.Tasks;
using DagAir.Facilities.Contracts.DTOs;
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
        
        [HttpGet]
        public async Task<IActionResult> Room(Guid uniqueRoomId)
        {
            var environment = _configuration.GetSection($"{ConfigurationSection}").Value;
            _logger.LogInformation($"Current environment: {environment}");

            var roomModel = new UniqueRoomModel();
            roomModel.UniqueRoomId = uniqueRoomId;
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
    
    public class UniqueRoomModel
    {
        public string Environment { get; set; }
        public Guid UniqueRoomId { get; set; }
    }
}