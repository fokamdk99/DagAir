#nullable enable
namespace DagAir.QueryNode.Data.AppEntitities
{
    public class Address : AuditableEntity
    {
        public long Id { get; set; }
        public long CountryId { get; set; }
        public long CityId { get; set; }
        public long PostalCodeId { get; set; }
        public virtual Country Country { get; set; }
        public virtual City City { get; set; }
        public virtual PostalCode PostalCode { get; set; }
        public virtual Organization? Organization { get; set; }
        public virtual Producer? Producer { get; set; }
    }
}