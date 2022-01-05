using System;
using System.Threading.Tasks;
using DagAir.Addresses.Contracts.DTOs;
using DagAir.AdminNode.Contracts.DTOs;
using DagAir.Facilities.Contracts.DTOs;
using DagAir.WebAdminApp.Affiliates;
using DagAir.WebAdminApp.Rooms;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace DagAir.WebAdminApp.Controllers
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
            var uniqueRoomModel = new UniqueRoomModel()
            {
                RoomDto = new RoomDto()
            };
            uniqueRoomModel.RoomDto.AffiliateId = affiliateId;

            return View(uniqueRoomModel);
        }

        [HttpPost]
        public async Task<IActionResult> AddNewRoom(UniqueRoomModel uniqueRoomModel)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            
            var newRoom = await _roomsHandler.AddNewRoom(uniqueRoomModel);

            if (newRoom == null)
            {
                string message =
                    $"Room with number {uniqueRoomModel.RoomDto.Number} was already added to the affiliate. "+
                "Please choose other number";
                ModelState.AddModelError(string.Empty, message);

                return View(uniqueRoomModel);
            }

            
            var adminNodeAffiliateDto = await _affiliatesHandler.GetAffiliateById(uniqueRoomModel.RoomDto.AffiliateId);

            var getAffiliateModel = new GetAffiliateModel();
            getAffiliateModel.AdminNodeAffiliateDto = adminNodeAffiliateDto;

            return View("~/Views/Affiliates/Affiliate.cshtml", getAffiliateModel);
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
        public RoomDto RoomDto { get; set; }
        public AdminNodeAffiliateDto AdminNodeAffiliateDto { get; set; }
    }
}