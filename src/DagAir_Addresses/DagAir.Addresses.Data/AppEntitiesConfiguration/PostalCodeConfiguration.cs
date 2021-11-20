using DagAir.Addresses.Data.AppEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DagAir.Addresses.Data.AppEntitiesConfiguration
{
    public class PostalCodeConfiguration : IEntityTypeConfiguration<PostalCode>
    {
        public void Configure(EntityTypeBuilder<PostalCode> builder)
        {
            builder.ConfigureBase();
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Number)
                .IsRequired();
            
            builder.HasMany(e => e.Addresses)
                .WithOne(x => x.PostalCode);
        }
    }
}