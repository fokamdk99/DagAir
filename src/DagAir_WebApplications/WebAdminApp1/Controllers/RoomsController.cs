using System.Threading.Tasks;
using DagAir.AdminNode.Contracts.DTOs;
using DagAir.Facilities.Contracts.DTOs;
using DagAir.Policies.Contracts.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using WebAdminApp1.Affiliates;
using WebAdminApp1.Affiliates.Models;
using WebAdminApp1.Rooms;
using WebAdminApp1.Rooms.Models;

namespace WebAdminApp1.Controllers
{
    [Authorize]
    public class RoomsController : Controller
    {
        private readonly IRoomsHandler _roomsHandler; 
        private readonly ILogger<RoomsController> _logger;
        private readonly IConfiguration _configuration;
        private readonly IAffiliatesHandler _affiliatesHandler;
        private const string ConfigurationSection = "environment";
        
        public RoomsController(ILogger<RoomsController> logger, 
            IConfiguration configuration, 
            IRoomsHandler roomsHandler, 
            IAffiliatesHandler affiliatesHandler)
        {
            _logger = logger;
            _configuration = configuration;
            _roomsHandler = roomsHandler;
            _affiliatesHandler = affiliatesHandler;
        }

        [HttpGet]
        public async Task<IActionResult> Room(long roomId)
        {
            var environment = _configuration.GetSection($"{ConfigurationSection}").Value;
            _logger.LogInformation($"Current environment: {environment}");

            var roomModel = new RoomModel();
            var adminNodeRoomDto = await _roomsHandler.GetRoom(roomId);
            roomModel.AdminNodeRoomDto = adminNodeRoomDto;
            
            if (environment == "docker")
            {
                roomModel.Environment = "appsettings.Docker.json";
                
                return View(roomModel);
            }
            
            if (environment == "kubernetes")
            {
                roomModel.Environment = "appsettings.Kubernetes.json";
                
                return View(roomModel);
            }
            
            roomModel.Environment = "appsettings.json";

            return View(roomModel);
        }
        
        [HttpGet]
        public async Task<IActionResult> AddNewRoom(long affiliateId)
        {
            var roomModel = new RoomModel()
            {
                AdminNodeRoomDto = new AdminNodeRoomDto()
            };

            roomModel.AdminNodeRoomDto.RoomDto = new RoomDto();
            roomModel.AdminNodeRoomDto.PastMeasurements = new PastMeasurementsDto();
            roomModel.AdminNodeRoomDto.RoomDto.AffiliateId = affiliateId;

            return View(roomModel);
        }

        [HttpPost]
        public async Task<IActionResult> AddNewRoom(RoomModel roomModel)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            
            var newRoom = await _roomsHandler.AddNewRoom(roomModel);

            if (newRoom == null)
            {
                string message =
                    $"Room with number {roomModel.AdminNodeRoomDto.RoomDto.Number} was already added to the affiliate. "+
                "Please choose other number";
                ModelState.AddModelError(string.Empty, message);

                return View(roomModel);
            }

            
            var adminNodeAffiliateDto = await _affiliatesHandler.GetAffiliateById(roomModel.AdminNodeRoomDto.RoomDto.AffiliateId);

            var getAffiliateModel = new AffiliateModel();
            getAffiliateModel.AdminNodeAffiliateDto = adminNodeAffiliateDto;

            return View("~/Views/Affiliates/Affiliate.cshtml", getAffiliateModel);
        }

        [HttpGet]
        public async Task<IActionResult> DeleteRoom(long roomId, long affiliateId)
        {
            var result = await _roomsHandler.DeleteRoom(roomId);
            
            var adminNodeAffiliateDto = await _affiliatesHandler.GetAffiliateById(affiliateId);

            var getAffiliateModel = new AffiliateModel();
            getAffiliateModel.AdminNodeAffiliateDto = adminNodeAffiliateDto;

            return View("~/Views/Affiliates/Affiliate.cshtml", getAffiliateModel);
        }

    }
}