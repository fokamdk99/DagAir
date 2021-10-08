using DagAir.QueryNode.Data.AppEntitities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DagAir.QueryNode.Data.AppEntitiesConfiguration
{
    public class RoomConfiguration : IEntityTypeConfiguration<Room>
    {
        public void Configure(EntityTypeBuilder<Room> builder)
        {
            builder.ConfigureBase();
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Number)
                .IsRequired();

            builder.Property(e => e.Floor)
                .IsRequired();

            builder.HasOne(e => e.Affiliate)
                .WithMany(x => x.Rooms)
                .HasForeignKey(e => e.AffiliateId)
                .IsRequired();

            builder.HasMany(e => e.Sensors)
                .WithOne(x => x.Room);
        }
    }
}