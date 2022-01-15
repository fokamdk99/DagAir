using DagAir.Sensors.Data.AppEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DagAir.Sensors.Data.AppEntitiesConfiguration
{
    public class SensorConfiguration : IEntityTypeConfiguration<Sensor>
    {
        public void Configure(EntityTypeBuilder<Sensor> builder)
        {
            builder.ConfigureBase();
            builder.HasKey(e => e.Id);

            builder.HasIndex(e => e.SensorName)
                .IsUnique();
            
            builder.Property(e => e.LastDataSentDate);

            builder.Property(e => e.RoomId)
                .IsRequired();

            builder.HasOne(e => e.SensorModel)
                .WithMany(x => x.Sensors)
                .HasForeignKey(e => e.SensorModelId)
                .IsRequired();

            builder.Property(e => e.AffiliateId)
                .IsRequired();
        }
    }
}