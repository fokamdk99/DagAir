using System.Threading.Tasks;
using AutoMapper;
using DagAir.Addresses.Contracts.Commands;
using DagAir.Addresses.Data.AppContext;
using DagAir.Addresses.Data.AppEntities;
using Microsoft.EntityFrameworkCore;

namespace DagAir.Addresses.Addresses.Commands
{
    public class AddNewAddressCommandHandler : ICommandHandler<AddNewAddressCommand, Address>
    {
        private readonly IDagAirAddressesAppContext _context;
        private readonly IMapper _mapper;
        private readonly ICommandHandler<AddNewCityCommand, City> _cityCommandHandler;
        private readonly ICommandHandler<AddNewCountryCommand, Country> _countryCommandHandler;
        private readonly ICommandHandler<AddNewPostalCodeCommand, PostalCode> _postalCodeCommandHandler;

        public AddNewAddressCommandHandler(IDagAirAddressesAppContext context, 
            IMapper mapper, 
            ICommandHandler<AddNewCityCommand, City> cityCommandHandler, 
            ICommandHandler<AddNewCountryCommand, Country> countryCommandHandler, 
            ICommandHandler<AddNewPostalCodeCommand, PostalCode> postalCodeCommandHandler)
        {
            _context = context;
            _mapper = mapper;
            _cityCommandHandler = cityCommandHandler;
            _countryCommandHandler = countryCommandHandler;
            _postalCodeCommandHandler = postalCodeCommandHandler;
        }

        public async Task<Address> Handle(AddNewAddressCommand command)
        {
            var address = _mapper.Map<Address>(command.AddressDto);
            var city = _mapper.Map<City>(command.CityDto);
            var country = _mapper.Map<Country>(command.CountryDto);
            var postalCode = _mapper.Map<PostalCode>(command.PostalCodeDto);

            if (city.Id == 0)
            {
                var addNewCityCommand = new AddNewCityCommand
                {
                    CityDto = command.CityDto
                };

                var savedCity = await _cityCommandHandler.Handle(addNewCityCommand);

                address.City = savedCity;
                address.CityId = savedCity.Id;
            }
            else
            {
                city = await _context.Cities.SingleOrDefaultAsync(x => x.Id == city.Id);
                address.City = city;
                address.CityId = city.Id;
            }
            
            if (country.Id == 0)
            {
                var addNewCountryCommand = new AddNewCountryCommand
                {
                    CountryDto = command.CountryDto
                };

                var savedCountry = await _countryCommandHandler.Handle(addNewCountryCommand);
                
                address.Country = savedCountry;
                address.CountryId = savedCountry.Id;
            }
            else
            {
                country = await _context.Countries.SingleOrDefaultAsync(x => x.Id == country.Id);
                address.Country = country;
                address.CountryId = country.Id;
            }
            
            if (postalCode.Id == 0)
            {
                var addNewPostalCodeCommand = new AddNewPostalCodeCommand
                {
                    PostalCodeDto = command.PostalCodeDto
                };

                var savedPostalCode = await _postalCodeCommandHandler.Handle(addNewPostalCodeCommand);
                address.PostalCode = savedPostalCode;
                address.PostalCodeId = savedPostalCode.Id;
            }
            else
            {
                postalCode = await _context.PostalCodes.SingleOrDefaultAsync(x => x.Id == postalCode.Id);
                address.PostalCode = postalCode;
                address.PostalCodeId = postalCode.Id;
            }

            await _context.Addresses.AddAsync(address);
            await _context.SaveChangesAsync();

            return address;
        }
    }
}