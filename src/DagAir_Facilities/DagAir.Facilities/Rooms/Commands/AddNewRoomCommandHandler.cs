using System.Threading.Tasks;
using AutoMapper;
using DagAir.Facilities.Contracts.Commands;
using DagAir.Facilities.Data.AppContext;
using DagAir.Facilities.Data.AppEntitities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DagAir.Facilities.Rooms.Commands
{
    public class AddNewRoomCommandHandler : ICommandHandler<AddNewRoomCommand, Room>
    {
        private readonly IDagAirFacilitiesAppContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<AddNewRoomCommandHandler> _logger;

        public AddNewRoomCommandHandler(IDagAirFacilitiesAppContext context, 
            IMapper mapper, 
            ILogger<AddNewRoomCommandHandler> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Room> Handle(AddNewRoomCommand command)
        {
            var room = _mapper.Map<Room>(command.RoomDto);
            
            var foundRoom =
                await _context.Rooms.SingleOrDefaultAsync(x => x.Number == command.RoomDto.Number
                && x.AffiliateId == command.RoomDto.AffiliateId);

            if (foundRoom != null)
            {
                string message =
                    $"Error while creating new room. Room with number ${command.RoomDto.Number} already exists in " +
                    $"affiliate with id {command.RoomDto.AffiliateId}";
                _logger.LogError(message);
                return null;
            }
            
            await _context.Rooms.AddAsync(room);
            await _context.SaveChangesAsync();
            return room;
        }
    }
}