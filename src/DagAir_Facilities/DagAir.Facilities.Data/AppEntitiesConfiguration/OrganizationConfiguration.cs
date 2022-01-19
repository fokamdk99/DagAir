using DagAir.Facilities.Data.AppEntitities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DagAir.Facilities.Data.AppEntitiesConfiguration
{
    public class OrganizationConfiguration : IEntityTypeConfiguration<Organization>
    {
        public void Configure(EntityTypeBuilder<Organization> builder)
        {
            builder.ConfigureBase();
            builder.HasKey(e => e.Id);
            builder
                .HasIndex(e => e.Name)
                .IsUnique();

            builder.Property(e => e.Name)
                .HasMaxLength(128)
                .IsRequired();

            builder.HasMany(e => e.Affiliates)
                .WithOne(x => x.Organization)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Property(e => e.AddressId);
        }
        
    }
}