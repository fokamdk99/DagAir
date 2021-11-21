using System.Threading.Tasks;
using AutoMapper;
using DagAir.Policies.Contracts.Commands;
using DagAir.Policies.Data.AppContext;
using DagAir.Policies.Data.AppEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace DagAir.Policies.Policies.Commands
{
    public class AddNewRoomPolicyCommandHandler : ICommandHandler<AddNewRoomPolicyCommand>
    {
        private readonly IDagAirPoliciesAppContext _context;
        private readonly IMapper _mapper;
        
        public AddNewRoomPolicyCommandHandler(IDagAirPoliciesAppContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<RoomPolicy> Handle(AddNewRoomPolicyCommand command)
        {
            var roomPolicy = _mapper.Map<RoomPolicy>(command.RoomPolicyDto);
            var expectedRoomConditions = _mapper.Map<ExpectedRoomConditions>(command.ExpectedRoomConditionsDto);
            var roomPolicyCategory = _mapper.Map<RoomPolicyCategory>(command.RoomPolicyCategoryDto);
            
            if (expectedRoomConditions.Id == 0)
            {
                await _context.ExpectedRoomConditions.AddAsync(expectedRoomConditions);
                await _context.SaveChangesAsync();
            }
            else
            {
                expectedRoomConditions =
                    await _context.ExpectedRoomConditions.SingleOrDefaultAsync(x => x.Id == expectedRoomConditions.Id);
            }

            roomPolicy.ExpectedConditions = expectedRoomConditions;
            roomPolicy.Category =
                await _context.RoomPolicyCategories.SingleOrDefaultAsync(x => x.Id == roomPolicyCategory.Id);

            await _context.RoomPolicies.AddAsync(roomPolicy);
            await _context.SaveChangesAsync();

            return roomPolicy;
        }
    }
}