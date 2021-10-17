using DagAir.QueryNode.Data.AppEntitities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DagAir.Sensors.Data.AppEntitiesConfiguration
{
    public static class AuditableEntityConfiguration
    {
        public static void ConfigureBase<T>(this EntityTypeBuilder<T> builder) where T : AuditableEntity
        {
            builder.Property(e => e.Created)
                .HasDefaultValueSql("(GetDate())");

            builder.Property(e => e.Modified);
        }
    }
}