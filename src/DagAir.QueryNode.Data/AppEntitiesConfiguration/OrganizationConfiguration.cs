using DagAir.QueryNode.Data.AppEntitities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DagAir.QueryNode.Data.AppEntitiesConfiguration
{
    public class OrganizationConfiguration : IEntityTypeConfiguration<Organization>
    {
        public void Configure(EntityTypeBuilder<Organization> builder)
        {
            builder.ConfigureBase();
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Name)
                .HasMaxLength(128)
                .IsRequired();

            builder.HasMany(e => e.Affiliates)
                .WithOne(x => x.Organization);

            builder.HasOne(e => e.Address)
                .WithOne(x => x.Organization)
                .HasForeignKey<Organization>(e => e.AddressId)
                .IsRequired();
        }
        
    }
}