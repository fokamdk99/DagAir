using System;
using System.Threading.Tasks;
using AutoMapper;
using DagAir.Facilities.Contracts.Commands;
using DagAir.Facilities.Data.AppContext;
using DagAir.Facilities.Data.AppEntitities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DagAir.Facilities.Organizations.Commands
{
    public class AddNewOrganizationCommandHandler : ICommandHandler<AddNewOrganizationCommand, Organization>
    {
        private readonly IDagAirFacilitiesAppContext _context;
        private readonly ILogger<AddNewOrganizationCommandHandler> _logger;
        private readonly IMapper _mapper;

        public AddNewOrganizationCommandHandler(IDagAirFacilitiesAppContext context, 
            IMapper mapper, 
            ILogger<AddNewOrganizationCommandHandler> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Organization> Handle(AddNewOrganizationCommand command)
        {
            var organization = _mapper.Map<Organization>(command.OrganizationDto);
            var foundOrganization =
                await _context.Organizations.SingleOrDefaultAsync(x => x.Name == command.OrganizationDto.Name);

            /*
            if (foundOrganization != null)
            {
                string message =
                    $"Error while creating new organization. Organization with name ${command.OrganizationDto.Name} already exists";
                _logger.LogError(message);
                throw new Exception(message);
            }
            */

            if (foundOrganization != null)
            {
                return foundOrganization;
            }
            
            await _context.Organizations.AddAsync(organization);
            await _context.SaveChangesAsync();
            return organization;
        }
    }
}