namespace DagAir.Addresses.Contracts.DTOs
{
    public class AddressDto
    {
        public long Id { get; set; }
        public long CountryId { get; set; }
        public long CityId { get; set; }
        public long PostalCodeId { get; set; }
        public virtual CountryDto Country { get; set; }
        public virtual CityDto City { get; set; }
        public virtual PostalCodeDto PostalCode { get; set; }
    }
}