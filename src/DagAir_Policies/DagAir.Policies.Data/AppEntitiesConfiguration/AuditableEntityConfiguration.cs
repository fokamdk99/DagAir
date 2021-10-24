using DagAir.Policies.Data.AppEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DagAir.Policies.Data.AppEntitiesConfiguration
{
    public static class AuditableEntityConfiguration
    {
        public static void ConfigureBase<T>(this EntityTypeBuilder<T> builder) where T : AuditableEntity
        {
            builder.Property(e => e.Created)
                .HasDefaultValueSql("(CURRENT_DATE)");

            builder.Property(e => e.Modified);
        }
    }
}