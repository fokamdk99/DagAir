using DagAir.QueryNode.Data.AppEntitities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DagAir.QueryNode.Data.AppEntitiesConfiguration
{
    public class CityConfiguration : IEntityTypeConfiguration<City>
    {
        public void Configure(EntityTypeBuilder<City> builder)
        {
            builder.ConfigureBase();
            builder.HasKey(e => e.Id);
            
            builder.Property(e => e.Name)
                .IsRequired();
            
            builder.HasMany(e => e.Addresses)
                .WithOne(x => x.City);
        }
    }
}