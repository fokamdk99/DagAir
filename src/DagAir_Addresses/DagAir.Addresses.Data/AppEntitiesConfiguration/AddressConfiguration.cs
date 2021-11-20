using DagAir.Addresses.Data.AppEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DagAir.Addresses.Data.AppEntitiesConfiguration
{
    public class AddressConfiguration : IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> builder)
        {
            builder.ConfigureBase();
            builder.HasKey(e => e.Id);

            builder.HasOne(e => e.Country)
                .WithMany(x => x.Addresses)
                .HasForeignKey(e => e.CountryId)
                .IsRequired();
            
            builder.HasOne(e => e.City)
                .WithMany(x => x.Addresses)
                .HasForeignKey(e => e.CityId)
                .IsRequired();
            
            builder.HasOne(e => e.PostalCode)
                .WithMany(x => x.Addresses)
                .HasForeignKey(e => e.PostalCodeId)
                .IsRequired();
        }
    }
}