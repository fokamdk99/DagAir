using DagAir.Policies.Data.AppEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DagAir.Policies.Data.AppEntitiesConfiguration
{
    public class RoomPolicyConfiguration : IEntityTypeConfiguration<RoomPolicy>
    {
        public void Configure(EntityTypeBuilder<RoomPolicy> builder)
        {
            builder.HasKey(e => e.Id);
            builder.ConfigureBase();

            builder.Property(e => e.StartDate)
                .IsRequired();
            
            builder.Property(e => e.EndDate)
                .IsRequired();
            
            
            builder.Property(e => e.RepeatOn)
                .HasMaxLength(256)
                .IsRequired();

            builder.Property(e => e.ExpectedConditionsId)
                .IsRequired();
            
            builder.Property(e => e.RoomId)
                .IsRequired();

            builder.HasOne(e => e.ExpectedConditions)
                .WithMany(x => x.RoomPolicies)
                .HasForeignKey(e => e.ExpectedConditionsId)
                .IsRequired();
            
            builder.HasOne(e => e.Category)
                .WithMany(x => x.RoomPolicies)
                .HasForeignKey(e => e.CategoryId)
                .IsRequired();
        }
    }
}