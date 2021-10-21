using DagAir.Facilities.Data.AppEntitities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DagAir.Facilities.Data.AppEntitiesConfiguration
{
    public class AffiliateConfiguration : IEntityTypeConfiguration<Affiliate>
    {
        public void Configure(EntityTypeBuilder<Affiliate> builder)
        {
            builder.ConfigureBase();
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Name)
                .HasMaxLength(128)
                .IsRequired();

            builder.HasOne(e => e.Organization)
                .WithMany(x => x.Affiliates)
                .HasForeignKey(e => e.OrganizationId)
                .IsRequired();

            builder.HasMany(e => e.Rooms)
                .WithOne(x => x.Affiliate);
        }
    }
}