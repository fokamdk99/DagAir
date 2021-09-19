using DagAir.IngestionNode.Data.UserEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DagAir.IngestionNode.Data.UserEntitiesConfiguration
{
    public static class AuditableEntityConfiguration
    {
        public static void ConfigureBase<T>(this EntityTypeBuilder<T> builder) where T : AuditableEntity
        {
            builder.Property(e => e.Created)
                .HasDefaultValueSql("GetDate()")
                .HasComment("The date of entity creation (local)");

            builder.Property(e => e.Modified)
                .HasComment("The date of entity last modification (local)");
        }
    }
}