using System.Threading.Tasks;
using AutoMapper;
using DagAir.Policies.Contracts.Commands;
using DagAir.Policies.Data.AppContext;
using DagAir.Policies.Data.AppEntities;
using DagAir.Policies.Policies.Commands;

namespace DagAir.Policies.RoomConditions.Commands
{
    public class AddNewExpectedRoomConditionsCommandHandler : ICommandHandler<AddNewExpectedRoomConditionsCommand, ExpectedRoomConditions>
    {
        private readonly IDagAirPoliciesAppContext _context;
        private readonly IMapper _mapper;

        public AddNewExpectedRoomConditionsCommandHandler(IDagAirPoliciesAppContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ExpectedRoomConditions> Handle(AddNewExpectedRoomConditionsCommand command)
        {
            var expectedRoomConditions = _mapper.Map<ExpectedRoomConditions>(command.ExpectedRoomConditionsDto);
            await _context.ExpectedRoomConditions.AddAsync(expectedRoomConditions);
            await _context.SaveChangesAsync();
            return expectedRoomConditions;
        }
    }
}