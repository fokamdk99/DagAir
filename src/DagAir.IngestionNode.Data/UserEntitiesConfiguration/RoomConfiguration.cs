using DagAir.IngestionNode.Data.UserEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DagAir.IngestionNode.Data.UserEntitiesConfiguration
{
    public class RoomConfiguration : IEntityTypeConfiguration<Room>
    {
        public void Configure(EntityTypeBuilder<Room> builder)
        {
            builder.HasComment("Table containing rooms that a given user is assigned to and can modify their policy");

            builder.ConfigureBase();
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id)
                .HasComment("The id of the room");

            builder.HasKey(e => e.UserId);
            builder.Property(e => e.UserId)
                .HasComment("The user who owns the instance of the message");
            
            builder.Property(e => e.RoomLocation)
                .HasComment("The location of the room");
            
        }
    }
}