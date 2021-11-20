namespace DagAir.Addresses.Data.AppEntities
{
    public class Address : AuditableEntity
    {
        //TODO: zmienic strukture bazy danych tak, by organizacje i producenci przechowywali id adresu, pod ktorym sie znajduja
        public long Id { get; set; }
        public long CountryId { get; set; }
        public long CityId { get; set; }
        public long PostalCodeId { get; set; }
        public virtual Country Country { get; set; }
        public virtual City City { get; set; }
        public virtual PostalCode PostalCode { get; set; }
    }
}