using System.Threading;
using System.Threading.Tasks;
using DagAir.Addresses.Data.AppEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;

namespace DagAir.Addresses.Data.AppContext
{
    public interface IDagAirAddressesAppContext
    {
        public DatabaseFacade Database { get; }
        public IModel Model { get; }
        
        public DbSet<Address> Addresses { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<PostalCode> PostalCodes { get; set; }
        public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}