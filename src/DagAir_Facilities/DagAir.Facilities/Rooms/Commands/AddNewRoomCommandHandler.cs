using System.Threading.Tasks;
using AutoMapper;
using DagAir.Facilities.Contracts.Commands;
using DagAir.Facilities.Data.AppContext;
using DagAir.Facilities.Data.AppEntitities;

namespace DagAir.Facilities.Rooms.Commands
{
    public class AddNewRoomCommandHandler : ICommandHandler<AddNewRoomCommand, Room>
    {
        private readonly IDagAirFacilitiesAppContext _context;
        private readonly IMapper _mapper;

        public AddNewRoomCommandHandler(IDagAirFacilitiesAppContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Room> Handle(AddNewRoomCommand command)
        {
            var room = _mapper.Map<Room>(command.RoomDto);
            await _context.Rooms.AddAsync(room);
            await _context.SaveChangesAsync();
            return room;
        }
    }
}