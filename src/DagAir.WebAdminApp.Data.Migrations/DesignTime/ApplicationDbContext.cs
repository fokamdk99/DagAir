using System;
using Microsoft.EntityFrameworkCore;

namespace DagAir.WebAdminApp.Data.Migrations.Models
{
    public partial class ApplicationDbContext
    {
        // for checking that DI is getting a different instance each time when the dbcontext is injected in the context of a web request
        private Guid _instanceId = Guid.NewGuid();

        public static void AddBaseOptions(DbContextOptionsBuilder<ApplicationDbContext> builder, string connectionString)
        {
            if (builder == null)
                throw new ArgumentNullException(nameof(builder));

            if (string.IsNullOrWhiteSpace(connectionString))
                throw new ArgumentException("Connection string must be provided", nameof(connectionString));

            builder.UseMySQL(connectionString);
        }

        public static void AddBaseOptions(DbContextOptionsBuilder builder, string connectionString)
        {
            if (builder == null)
                throw new ArgumentNullException(nameof(builder));

            if (string.IsNullOrWhiteSpace(connectionString))
                throw new ArgumentException("Connection string must be provided", nameof(connectionString));

            builder.UseMySQL(connectionString);
        }
    }
}