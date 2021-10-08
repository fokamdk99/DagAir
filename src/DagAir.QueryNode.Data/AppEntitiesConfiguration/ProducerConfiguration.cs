using DagAir.QueryNode.Data.AppEntitities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DagAir.QueryNode.Data.AppEntitiesConfiguration
{
    public class ProducerConfiguration : IEntityTypeConfiguration<Producer>
    {
        public void Configure(EntityTypeBuilder<Producer> builder)
        {
            builder.ConfigureBase();
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Name)
                .IsRequired();

            builder.Property(e => e.DateOfEstablishment);

            builder.HasOne(e => e.Address)
                .WithOne(x => x.Producer)
                .HasForeignKey<Producer>(e => e.AddressId)
                .IsRequired();

            builder.HasMany(e => e.SensorModels)
                .WithOne(x => x.Producer);
        }
    }
}