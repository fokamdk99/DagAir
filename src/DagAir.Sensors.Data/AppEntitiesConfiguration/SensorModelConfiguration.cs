using DagAir.QueryNode.Data.AppEntitities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DagAir.Sensors.Data.AppEntitiesConfiguration
{
    public class SensorModelConfiguration : IEntityTypeConfiguration<SensorModel>
    {
        public void Configure(EntityTypeBuilder<SensorModel> builder)
        {
            builder.ConfigureBase();
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Name)
                .HasMaxLength(128)
                .IsRequired();

            builder.Property(e => e.Version)
                .IsRequired();

            builder
                .HasOne(e => e.Producer)
                .WithMany(x => x.SensorModels)
                .HasForeignKey(e => e.ProducerId)
                .IsRequired();

            builder.HasMany(e => e.Sensors)
                .WithOne(x => x.SensorModel);
        }
    }
}