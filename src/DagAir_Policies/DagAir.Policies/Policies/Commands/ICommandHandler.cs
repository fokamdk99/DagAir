using System.Threading.Tasks;
using DagAir.Policies.Contracts.Commands;
using DagAir.Policies.Data.AppEntities;

namespace DagAir.Policies.Policies.Commands
{
    public interface ICommandHandler<T> where T : ICommand
    {
        Task<RoomPolicy> Handle(T command);
    }
}