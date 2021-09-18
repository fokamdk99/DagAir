using System.Collections.Generic;

namespace DagAir.IngestionNode.Contracts
{
    public interface IOrganization
    {
        string Id { get; }
        string Name { get; }
        ICollection<IDivision> Divisions { get; } 
    }
}