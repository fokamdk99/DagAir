using System.Threading.Tasks;
using AutoMapper;
using DagAir.Policies.Contracts.Commands;
using DagAir.Policies.Data.AppContext;
using DagAir.Policies.Data.AppEntities;
using Microsoft.EntityFrameworkCore;

namespace DagAir.Policies.Policies.Commands
{
    public class AddNewRoomPolicyCommandHandler : ICommandHandler<AddNewRoomPolicyCommand, RoomPolicy>
    {
        private readonly IDagAirPoliciesAppContext _context;
        private readonly IMapper _mapper;
        private readonly ICommandHandler<AddNewExpectedRoomConditionsCommand, ExpectedRoomConditions> _expectedRoomConditionscommandHandler;
        
        public AddNewRoomPolicyCommandHandler(IDagAirPoliciesAppContext context, IMapper mapper, ICommandHandler<AddNewExpectedRoomConditionsCommand, ExpectedRoomConditions> expectedRoomConditionscommandHandler)
        {
            _context = context;
            _mapper = mapper;
            _expectedRoomConditionscommandHandler = expectedRoomConditionscommandHandler;
        }

        public async Task<RoomPolicy> Handle(AddNewRoomPolicyCommand command)
        {
            var roomPolicy = _mapper.Map<RoomPolicy>(command.RoomPolicyDto);
            var expectedRoomConditions = _mapper.Map<ExpectedRoomConditions>(command.ExpectedRoomConditionsDto);
            var roomPolicyCategory = _mapper.Map<RoomPolicyCategory>(command.RoomPolicyCategoryDto);
            
            if (expectedRoomConditions.Id == 0)
            {
                var addNewExpectedConditionsCommand = new AddNewExpectedRoomConditionsCommand
                {
                    ExpectedRoomConditionsDto = command.ExpectedRoomConditionsDto
                };
                var savedExpectedRoomConditions = await _expectedRoomConditionscommandHandler.Handle(addNewExpectedConditionsCommand);

                roomPolicy.ExpectedConditions = savedExpectedRoomConditions;
                roomPolicy.ExpectedConditionsId = savedExpectedRoomConditions.Id;
            }
            else
            {
                expectedRoomConditions =
                    await _context.ExpectedRoomConditions.SingleOrDefaultAsync(x => x.Id == expectedRoomConditions.Id);
                
                roomPolicy.ExpectedConditions = expectedRoomConditions;
                roomPolicy.ExpectedConditionsId = expectedRoomConditions.Id;
            }
            
            var category = await _context.RoomPolicyCategories.SingleOrDefaultAsync(x => x.Id == roomPolicyCategory.Id);

            roomPolicy.Category = category;
            roomPolicy.CategoryId = category.Id;
            
            await _context.RoomPolicies.AddAsync(roomPolicy);
            await _context.SaveChangesAsync();

            return roomPolicy;
        }
    }
}