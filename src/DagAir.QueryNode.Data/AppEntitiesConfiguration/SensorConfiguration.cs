using DagAir.QueryNode.Data.AppEntitities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DagAir.QueryNode.Data.AppEntitiesConfiguration
{
    public class SensorConfiguration : IEntityTypeConfiguration<Sensor>
    {
        public void Configure(EntityTypeBuilder<Sensor> builder)
        {
            builder.ConfigureBase();
            builder.HasKey(e => e.Id);

            builder.Property(e => e.LastDataSentDate);

            builder.HasOne(e => e.Room)
                .WithMany(x => x.Sensors)
                .HasForeignKey(e => e.RoomId);

            builder.HasOne(e => e.SensorModel)
                .WithMany(x => x.Sensors)
                .HasForeignKey(e => e.SensorModelId)
                .IsRequired();
            
            builder.HasOne(e => e.Affiliate)
                .WithMany(x => x.Sensors)
                .HasForeignKey(e => e.AffiliateId);
        }
    }
}