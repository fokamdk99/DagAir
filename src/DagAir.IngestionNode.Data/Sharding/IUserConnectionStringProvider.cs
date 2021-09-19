using System.Collections.Generic;
using DagAir.IngestionNode.Contracts;

namespace DagAir.IngestionNode.Data.Sharding
{
    public interface IUserConnectionStringProvider
    {
        string Provide(UserIdentity user);
        IEnumerable<ConnectionString> ProvideAll();
    }
}