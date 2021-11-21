using DagAir.Policies.Data.AppEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DagAir.Policies.Data.AppEntitiesConfiguration
{
    public class ExpectedRoomConditionsConfiguration : IEntityTypeConfiguration<ExpectedRoomConditions>
    {
        public void Configure(EntityTypeBuilder<ExpectedRoomConditions> builder)
        {
            builder.ConfigureBase();
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Temperature)
                .IsRequired();
            
            builder.Property(e => e.Humidity)
                .IsRequired();
            
            builder.Property(e => e.Illuminance)
                .IsRequired();

            builder.Property(e => e.TemperatureMargin);
            builder.Property(e => e.IlluminanceMargin);
            builder.Property(e => e.HumidityMargin);
            builder.Property(e => e.RoomPolicyId);

            builder.HasMany(e => e.RoomPolicies)
                .WithOne(x => x.ExpectedConditions);
        }
    }
}