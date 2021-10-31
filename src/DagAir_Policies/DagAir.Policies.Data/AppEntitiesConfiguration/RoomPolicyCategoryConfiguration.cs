using DagAir.Policies.Data.AppEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DagAir.Policies.Data.AppEntitiesConfiguration
{
    public class RoomPolicyCategoryConfiguration : IEntityTypeConfiguration<RoomPolicyCategory>
    {
        public void Configure(EntityTypeBuilder<RoomPolicyCategory> builder)
        {
            builder.ConfigureBase();
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Name)
                .HasMaxLength(128)
                .IsRequired();

            builder.Property(e => e.CategoryNumber);

            builder.HasMany(e => e.RoomPolicies)
                .WithOne(x => x.Category);
            
            builder.Property(e => e.Created)
                .IsRequired();

            builder.Property(e => e.Modified);
        }
    }
}